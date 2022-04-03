using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sack : MonoBehaviour
{
    public int ID;

    public UIManager UI;
    public SpawnItems Spawner;

    public Image HatPlaceholder;
    public Image MutationImage;
    public Image ExecutedImage;

    public TextMeshProUGUI Text;

    public Sprite TournamentHat01;
    public Sprite TournamentHat02;

    public Sprite OpenStorage;
    public Sprite CloseStorage;

    private Chromosome CurrentChromossome;

    private Button myButton;
    private Image myImage;

    private bool isOpen = false;

    void Awake()
    {
        HatPlaceholder.gameObject.SetActive(false);
        MutationImage.gameObject.SetActive(false);
        ExecutedImage.gameObject.SetActive(false);
        Text.text = "";

        myButton = GetComponent<Button>();
        myImage = GetComponent<Image>();

        myImage.sprite = CloseStorage;

        myButton.onClick.AddListener(() =>
        {
            print(CurrentChromossome.ToString());

            if (!isOpen)
            {
                UI.CloseAllExceptFor(ID);
                Spawner.Spawn(CurrentChromossome.Alleles);
                UI.GeracoesText.gameObject.SetActive(false);
                isOpen = true;
            } else
            {
                UI.OpenAll();
                Spawner.RemoveFromScene();
                UI.GeracoesText.gameObject.SetActive(true);
                isOpen = false;
            }
        });
    }

    public void SetChromossome(Chromosome chromossome)
    {
        CurrentChromossome = chromossome;
        ID = CurrentChromossome.ID;
        //ResetStatus();

        Text.text = $"Weight: {CurrentChromossome.TotalWeight()}\nValue: {CurrentChromossome.TotalValue()}\nFitness: {string.Format("{0:0.00}", CurrentChromossome.Fitness())}";
    }

    public void ResetStatus()
    {
        HatPlaceholder.gameObject.SetActive(false);
        MutationImage.gameObject.SetActive(false);
        ExecutedImage.gameObject.SetActive(false);
    }

    public void SetExecuted()
    {
        ExecutedImage.gameObject.SetActive(true);
    }

    public void SetParticipant(bool isParticipant, int number = 0)
    {
        if (number == 0)
        {
            HatPlaceholder.sprite = TournamentHat01;
        } else
        {
            HatPlaceholder.sprite = TournamentHat02;
        }

        HatPlaceholder.gameObject.SetActive(isParticipant);
    }

    public void SetMutation(bool isMutation)
    {
        MutationImage.gameObject.SetActive(isMutation);
    }
}
