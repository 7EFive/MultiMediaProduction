using UnityEngine.UI;
using UnityEngine;

public class DialougeTest : MonoBehaviour
{
    public int i;
    public bool canInteract = true;
    // Other Objects
    [SerializeField] GameObject playerObj;
    PlayerMain player;
    bool isInZone;
    [SerializeField] GameObject dummy;
    [SerializeField] GameObject interaction;

    public GameObject icon1;
    public GameObject icon2;

    [SerializeField] GameObject Vixie_TextInteract1;
    [SerializeField] GameObject Vixie_TextInteract2;
    [SerializeField] GameObject Vixie_TextInteract3;
    [SerializeField] GameObject Vixie_TextInteract4;
    [SerializeField] GameObject Vixie_TextInteract5;
    [SerializeField] GameObject Vixie_TextInteract6;
    [SerializeField] GameObject Vixie_TextInteract7;
    [SerializeField] GameObject Vixie_TextInteract8;
    [SerializeField] GameObject Vixie_TextInteract9;
    [SerializeField] GameObject Vixie_TextInteract10;
    [SerializeField] GameObject Vixie_TextInteract11;
    [SerializeField] GameObject Vixie_TextInteract12;
    [SerializeField] GameObject Vixie_TextInteract13;
    [SerializeField] GameObject Vixie_TextInteract14;
    [SerializeField] GameObject Vixie_TextInteract15;

    [SerializeField] GameObject Dummy_TextInteract1;
    [SerializeField] GameObject Dummy_TextInteract2;
    [SerializeField] GameObject Dummy_TextInteract3;
    [SerializeField] GameObject Dummy_TextInteract4;
    [SerializeField] GameObject Dummy_TextInteract5;
    [SerializeField] GameObject Dummy_TextInteract6;
    [SerializeField] GameObject Dummy_TextInteract7;
    [SerializeField] GameObject Dummy_TextInteract8;
    [SerializeField] GameObject Dummy_TextInteract9;
    [SerializeField] GameObject Dummy_TextInteract10;
    [SerializeField] GameObject Dummy_TextInteract11;
    [SerializeField] GameObject Dummy_TextInteract12;
    [SerializeField] GameObject Dummy_TextInteract13;
    [SerializeField] GameObject Dummy_TextInteract14;
    public GameObject nameV;
    public GameObject nameD;
    public GameObject name1;
    public GameObject name2a;
    public GameObject name2b;
    public SceneInfo scene;

    public GameObject bg;


    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<PlayerMain>();
        i = 0;
        name1.SetActive(true);
        name2a.SetActive(true);
        name2b.SetActive(false);
        nameV.SetActive(false);
        nameD.SetActive(false);

        icon1.SetActive(false);
        icon2.SetActive(false);
        bg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!scene.wasDestroyed && isInZone)
        {
            if (player.interactionStun)
            {
                bg.SetActive(true);
            }
            else
            {
                bg.SetActive(false);
            }
            mainDialog();
            if (Input.GetKeyUp(KeyCode.E))
            {
                canInteract = true;
            }
        }
        
    }
    void mainDialog()
    {
        if (i == 32)
        {
            i = 28;
        }

        if (Input.GetKeyDown(KeyCode.E) && canInteract && isInZone)
        {

            switch (i)
            {
                case 0:
                    icon2.SetActive(true);
                    player.interactionStun = true;
                    Dummy_TextInteract1.SetActive(true);
                    ++i;
                    canInteract = false;
                    interaction.SetActive(false);
                    break;
                case 1:
                    //Vixie acknowledge the dummy talking
                    icon1.SetActive(true);
                    icon2.SetActive(false);
                    Dummy_TextInteract1.SetActive(false);
                    Vixie_TextInteract1.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 2:
                    icon1.SetActive(false);
                    icon2.SetActive(true);
                    name2a.SetActive(false);
                    name2b.SetActive(true);
                    //Dummy: i wish that i could say that too
                    Vixie_TextInteract1.SetActive(false);
                    Dummy_TextInteract2.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 3:
                    icon1.SetActive(true);
                    icon2.SetActive(false);
                    //Vixie: nothing personal
                    Dummy_TextInteract2.SetActive(false);
                    Vixie_TextInteract2.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 4:
                    //Vixie : let alone...
                    Vixie_TextInteract2.SetActive(false);
                    Vixie_TextInteract3.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 5:
                    icon1.SetActive(false);
                    icon2.SetActive(true);
                    //Dummy: fair enough
                    Vixie_TextInteract3.SetActive(false);
                    Dummy_TextInteract3.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 6:
                    //Dummy: hey how did you do that...
                    Dummy_TextInteract3.SetActive(false);
                    Dummy_TextInteract4.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 7:
                    icon1.SetActive(true);
                    icon2.SetActive(false);
                    //Vixie: Let's just says that it is my expertise
                    Dummy_TextInteract4.SetActive(false);
                    Vixie_TextInteract4.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 8:
                    icon1.SetActive(false);
                    icon2.SetActive(true);
                    //Dummy: i couldn't bealeve, that someone ELSE could avoid and maybe ALSO place those defense spells like mine
                    Vixie_TextInteract4.SetActive(false);
                    Dummy_TextInteract5.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 9:
                    icon1.SetActive(true);
                    icon2.SetActive(false);
                    //Vixie: padon me? 
                    Dummy_TextInteract5.SetActive(false);
                    Vixie_TextInteract5.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 10:
                    //Vixie: i mean i am found of thier existance, since it knowladge is crusual for exterminating cursed time relicts...  
                    Vixie_TextInteract5.SetActive(false);
                    Vixie_TextInteract6.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 11:
                    //Vixie: But even I don't know how or why they are palce on something...
                    Vixie_TextInteract6.SetActive(false);
                    Vixie_TextInteract7.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 12:
                    //Vixie: Know when i thing of it, i should of ivestiagated you back then.
                    Vixie_TextInteract7.SetActive(false);
                    Vixie_TextInteract8.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 13:
                    //Vixie: Did you put the spell on yourself or was there someone else involved? And how did you arrived here (let alone beeing alive)?
                    Vixie_TextInteract8.SetActive(false);
                    Vixie_TextInteract9.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 14:
                    icon1.SetActive(false);
                    icon2.SetActive(true);
                    //Dummy: Hehe HeHe Heeee..., so many personal questions, which i sadly am not aloud to answer...
                    Vixie_TextInteract9.SetActive(false);
                    Dummy_TextInteract6.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 15:
                    icon1.SetActive(true);
                    icon2.SetActive(false);
                    //Vixie: Not allowed...? So someone was involved in it?
                    Dummy_TextInteract6.SetActive(false);
                    Vixie_TextInteract10.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 16:
                    icon1.SetActive(false);
                    icon2.SetActive(true);
                    //Dummy: I would like you to restrain yourself digging into that matter.
                    Vixie_TextInteract10.SetActive(false);
                    Dummy_TextInteract7.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 17:
                    icon1.SetActive(true);
                    icon2.SetActive(false);
                    //Vixie: Whatever... , but do you at least have a name or are you just an yapping scarecrow?
                    Dummy_TextInteract7.SetActive(false);
                    Vixie_TextInteract11.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 18:
                    icon1.SetActive(false);
                    icon2.SetActive(true);
                    //Dummy: To your suprize, i actualy get called "Dan", "Dummy" or "Dummy Dan", so i think it's what you could categorize as "names"... .
                    Vixie_TextInteract11.SetActive(false);
                    Dummy_TextInteract8.SetActive(true);
                    ++i;
                    canInteract = false;
                    nameD.SetActive(true);
                    name2b.SetActive(false);
                    break;
                case 19:
                    icon1.SetActive(true);
                    icon2.SetActive(false);
                    //Vixie: So you got even nicknames? Someone is involved in those spells... .
                    Dummy_TextInteract8.SetActive(false);
                    Vixie_TextInteract12.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 20:
                    icon1.SetActive(false);
                    icon2.SetActive(true);
                    //Dummy: I'll prefer relly not to speak, if i speak them i will be in big trouble, in BIG Trouble and i defenetly don't want to be in big trouble!
                    Vixie_TextInteract12.SetActive(false);
                    Dummy_TextInteract9.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 21:
                    //Dummy: Well at least tell me what your name isá since i dished so many thing about me...
                    Dummy_TextInteract9.SetActive(false);
                    Dummy_TextInteract10.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 22:
                    icon1.SetActive(true);
                    icon2.SetActive(false);
                    //Vixie: (i think it won't be crucial if i keep it brief)
                    Dummy_TextInteract10.SetActive(false);
                    Vixie_TextInteract13.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 23:
                    //Vixie: The name is Vic... "Vixie" and let's just say i do some "cleaning" here.
                    Vixie_TextInteract13.SetActive(false);
                    Vixie_TextInteract14.SetActive(true);
                    ++i;
                    canInteract = false;
                    nameV.SetActive(true);
                    name1.SetActive(false);
                    break;
                case 24:
                    icon1.SetActive(false);
                    icon2.SetActive(true);
                    //Dummy: Well nice to meet you Vixie, i sadly have to revaluate the way i'm communiacting, so i won't get my self deeper into to the mud, i hope you can underastnd that.
                    Vixie_TextInteract14.SetActive(false);
                    Dummy_TextInteract11.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 25:
                    //Dummy: Well nice to meet you Vixie, i sadly have to revaluate the way i'm communiacting, so i won't get my self deeper into to the mud, i hope you can underastnd that.
                    Dummy_TextInteract11.SetActive(false);
                    Dummy_TextInteract12.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 26:
                    icon1.SetActive(true);
                    icon2.SetActive(false);
                    //Vixie: Alright ("I don't think that there is any more information to gain").
                    Dummy_TextInteract12.SetActive(false);
                    Vixie_TextInteract15.SetActive(true);
                    ++i;
                    canInteract = false;
                    break;
                case 27:
                    //Dialog 1 end
                    icon1.SetActive(false);
                    Vixie_TextInteract15.SetActive(false);
                    ++i;
                    canInteract = false;
                    player.interactionStun = false;
                    break;
                case 28:
                    //
                    Dummy_TextInteract13.SetActive(true);
                    icon2.SetActive(true);
                    ++i;
                    canInteract = false;
                    player.interactionStun = true;
                    break;
                case 29:
                    //
                    Dummy_TextInteract13.SetActive(false);
                    icon2.SetActive(false);
                    ++i;
                    canInteract = false;
                    player.interactionStun = false;
                    break;
                case 30:
                    //
                    Dummy_TextInteract14.SetActive(true);
                    icon2.SetActive(true);
                    ++i;
                    canInteract = false;
                    player.interactionStun = true;
                    break;
                case 31:
                    //
                    Dummy_TextInteract14.SetActive(false);
                    icon2.SetActive(false);
                    ++i;
                    canInteract = false;
                    player.interactionStun = false;
                    break;
                

                default:
                    //Dummy  starts Talking
                    
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = true;
            //interaction.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = false;
            //interaction.SetActive(false);
        }
    }
}
