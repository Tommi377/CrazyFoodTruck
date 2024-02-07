using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance;

    [SerializeField] private Player player;
    [SerializeField] private CarPlayer car;

    [SerializeField] private Transform kitchenTransform;
    [SerializeField] private Transform carTransform;
    [SerializeField] private Transform carCanonTransform;
    [SerializeField] private Transform middleHUD;

    [SerializeField] private bool kitchenMode = false;
    [SerializeField] private bool skipTutorial = false;

    [SerializeField] private Transform supplyItemContainerTransform;
    [SerializeField] private GameObject supplyItemPrefab;

    [SerializeField] private InteriorSetup interiorSetup;
    [SerializeField] private TutorialUI tutorialUI;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text mainMiddleText;
    [SerializeField] private TMP_Text subMiddleText;
    [SerializeField] private List<GameObject> timeTickersGameObjects;
    private List<ITickTime> timeTickers = new List<ITickTime>();

    private Dictionary<IngredientSO, List<SupplyPoint>> ingredientSupplyPointMap = new Dictionary<IngredientSO, List<SupplyPoint>>();

    public Player Player => player;
    public CarPlayer Car => car;

    public Vector3 KitchenOffset { get; private set; } = new Vector3(-2f, -4f, 0f);
    public int RequiredDeliveries { get {
            switch(Level) {
                case 1:
                    return 2;
                case 2:
                    return 4;
                default:
                    return 5;
            }
        }
    }

    private List<GameObject> deleteAtLevelEnd = new List<GameObject>();

    public int Level { get; private set; } = 1;
    public float DifficultyModifier { get; private set; } = 1;
    public bool InGame { get; private set; } = false;

    public bool CityMode => !kitchenMode;
    public bool KitchenMode => kitchenMode;

    public bool GPSOn { get; private set; } = false;
    public Color GPSColor { get; private set; } = Color.red;
    public Vector3 TrackedPoint { get; private set; }

    public bool InfiniteMode => Level == 5;

    private void Awake() {
        Instance = this;

        Level = StaticData.Level;
        DifficultyModifier = StaticData.Difficulty;
        skipTutorial = StaticData.SkipTutorial;

        SetMode();

        timeTickersGameObjects.ForEach(go => timeTickers.Add(go.GetComponent<ITickTime>()));
    }

    private void Start() {
        tutorialUI.PrepareLevel(Level, skipTutorial);
    }

    private void Update() {
        if (InGame) {
            if (InfiniteMode) {
                scoreText.SetText("Deliveries: " + DeliveryManager.Instance.DeliveriesDelivered);
            } else {
                scoreText.SetText("Deliveries: " + DeliveryManager.Instance.DeliveriesDelivered + "/" + RequiredDeliveries);
            }
            timeTickers.ForEach(t => t.TickTime());

            if (!InfiniteMode && DeliveryManager.Instance.DeliveriesDelivered >= RequiredDeliveries) {
                middleHUD.gameObject.SetActive(true);
                mainMiddleText.SetText("Day Over");
                subMiddleText.SetText("Starting Next Day");
                InGame = false;
                StartCoroutine(LevelEndCoroutine(true));
            }

            if (DeliveryManager.Instance.DeliveriesFailed > 0) {
                middleHUD.gameObject.SetActive(true);
                mainMiddleText.SetText("Fired :(");
                subMiddleText.SetText(InfiniteMode ? "Restarting Day" : "Delivered:" + DeliveryManager.Instance.DeliveriesDelivered);
                InGame = false;

                if (InfiniteMode && DeliveryManager.Instance.DeliveriesDelivered > PlayerPrefs.GetInt("Highscore", 0)) {
                    PlayerPrefs.SetInt("Highscore", DeliveryManager.Instance.DeliveriesDelivered);
                }
                StartCoroutine(LevelEndCoroutine(false));
            }
        }
    }

    IEnumerator LevelEndCoroutine(bool win) {
        yield return new WaitForSeconds(3);
        if (win) {
            NextLevel();
        } else {
            ResetLevel();
        }
        middleHUD.gameObject.SetActive(false);
    }

    public void StartLevel(int level) {
        InGame = true;
        Level = level;

        DeliveryManager.Instance.LevelStart(level);
        interiorSetup.InitializeInterior(level);

        foreach (var ingredient in ingredientSupplyPointMap.Keys) {
            if (ingredient.Level <= level) {
                SupplyItemUI supplyItem = Instantiate(supplyItemPrefab, supplyItemContainerTransform).GetComponent<SupplyItemUI>();
                supplyItem.Init(ingredient);
                deleteAtLevelEnd.Add(supplyItem.gameObject);
            }
        }
    }

    public void ResetLevel() {
        if (KitchenMode) {
            player.transform.localPosition = new Vector3(5.5f, 0.4f, 0);
            ToggleMode();
        }

        DeliveryManager.Instance.LevelEnd();
        InGame = false;

        tutorialUI.PrepareLevel(Level, false);
        deleteAtLevelEnd.ForEach(go => Destroy(go));
    }

    public void NextLevel() {
        if (KitchenMode) {
            player.transform.localPosition = new Vector3(5.5f, 0.4f, 0);
            ToggleMode();
        }

        DeliveryManager.Instance.LevelEnd();
        InGame = false;
        Level += 1;

        tutorialUI.PrepareLevel(Level, skipTutorial);
        deleteAtLevelEnd.ForEach(go => Destroy(go));

        if (InfiniteMode) {
            DifficultyModifier = 1;
            skipTutorial = false;
        }
    }

    public void RegisterSupplyPoint(SupplyPoint supplyPoint) {
        IngredientSO ingredient = supplyPoint.GetIngredient();
        if (!ingredientSupplyPointMap.ContainsKey(ingredient)) {
            ingredientSupplyPointMap[ingredient] = new List<SupplyPoint>();
        }
        
        ingredientSupplyPointMap[ingredient].Add(supplyPoint);
    }

    public void ToggleMode() {
        kitchenMode = !kitchenMode;
        SetMode();
    }

    public void SetMode() {
        if (kitchenMode) {
            kitchenTransform.position = carCanonTransform.position + KitchenOffset;
        }

        kitchenTransform.gameObject.SetActive(kitchenMode);
    }

    public void SetGPS(Vector3 point, Color color) {
        SoundManager.Instance.PlaySound(SoundManager.Sound.Click);
        GPSOn = true;
        GPSColor = color;
        TrackedPoint = point;
    }

    public void RemoveGPS() {
        GPSOn = false;
        TrackedPoint = default;
    }

    public SupplyPoint GetClosestSupplyPoint(IngredientSO ingredient) {
        if (ingredientSupplyPointMap.TryGetValue(ingredient, out var supplyPoints)) {
            SupplyPoint sp = null;
            float best = float.MaxValue;

            supplyPoints.ForEach(supplyPoint => {
                float diff = Vector2.Distance(car.transform.position, supplyPoint.transform.position);
                if (diff < best) {
                    sp = supplyPoint;
                    best = diff;
                }
            });

            return sp;
        } else {
            Debug.LogError("No supply point for ingredient " + ingredient.name);
            return null;
        }
    }
}
