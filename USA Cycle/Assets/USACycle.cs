using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class USACycle : MonoBehaviour {

   public KMBombInfo Bomb;
   public KMAudio Audio;

   public GameObject Flag;

   public KMSelectable Module;

   public TextMesh Submitting;

   public KMSelectable[] InitalButtons;
   public Sprite[] States;
   public SpriteRenderer[] ThreeStates;
   public SpriteRenderer[] TwoStates;

   List<int> StateIndexes = new List<int> { };
   List<int> Pages = new List<int> { };
   int PageIndex = 0;

   string[] DeclarationOfIndependence = "When in the Course of human events it becomes necessary for one people to dissolve the political bands which have connected them with another and to assume among the powers of the earth the separate and equal station to which the Laws of Nature and of Nature s God entitle them a decent respect to the opinions of mankind requires that they should declare the causes which impel them to the separation We hold these truths to be self evident that all men are created equal that they are endowed by their Creator with certain unalienable Rights that among these are Life Liberty and the pursuit of Happiness  That to secure these rights Governments are instituted among Men deriving their just powers from the consent of the governed  That whenever any Form of Government becomes destructive of these ends it is the Right of the People to alter or to abolish it and to institute new Government laying its foundation on such principles and organizing its powers in such form as to them shall seem most likely to effect their Safety and Happiness Prudence indeed will dictate that Governments long established should not be changed for light and transient causes and accordingly all experience hath shewn that mankind are more disposed to suffer while evils are sufferable than to right themselves by abolishing the forms to which they are accustomed But when a long train of abuses and usurpations pursuing invariably the same Object evinces a design to reduce them under absolute Despotism it is their right it is their duty to throw off such Government and to provide new Guards for their future security  Such has been the patient sufferance of these Colonies and such is now the necessity which constrains them to alter their former Systems of Government The history of the present King of Great Britain is a history of repeated injuries and usurpations all having in direct object the establishment of an absolute Tyranny over these States To prove this let Facts be submitted to a candid world He has refused his Assent to Laws the most wholesome and necessary for the public good He has forbidden his Governors to pass Laws of immediate and pressing importance unless suspended in their operation till his Assent should be obtained and when so suspended he has utterly neglected to attend to them He has refused to pass other Laws for the accommodation of large districts of people unless those people would relinquish the right of Representation in the Legislature a right inestimable to them and formidable to tyrants only He has called together legislative bodies at places unusual uncomfortable and distant from the depository of their public Records for the sole purpose of fatiguing them into compliance with his measures He has dissolved Representative Houses repeatedly for opposing with manly firmness his invasions on the rights of the people He has refused for a long time after such dissolutions to cause others to be elected whereby the Legislative powers incapable of Annihilation have returned to the People at large for their exercise the State remaining in the mean time exposed to all the dangers of invasion from without and convulsions within He has endeavoured to prevent the population of these States for that purpose obstructing the Laws for Naturalization of Foreigners refusing to pass others to encourage their migrations hither and raising the conditions of new Appropriations of Lands He has obstructed the Administration of Justice by refusing his Assent to Laws for establishing Judiciary powers He has made Judges dependent on his Will alone for the tenure of their offices and the amount and payment of their salaries He has erected a multitude of New Offices and sent hither swarms of Officers to harrass our people and eat out their substance He has kept among us in times of peace Standing Armies without the Consent of our legislatures He has affected to render the Military independent of and superior to the Civil power He has combined with others to subject us to a jurisdiction foreign to our constitution and unacknowledged by our laws giving his Assent to their Acts of pretended Legislation For Quartering large bodies of armed troops among us For protecting them by a mock Trial from punishment for any Murders which they should commit on the Inhabitants of these States For cutting off our Trade with all parts of the world For imposing Taxes on us without our Consent For depriving us in many cases of the benefits of Trial by Jury For transporting us beyond Seas to be tried for pretended offencesFor abolishing the free System of English Laws in a neighbouring Province establishing therein an Arbitrary government and enlarging its Boundaries so as to render it at once an example and fit instrument for introducing the same absolute rule into these Colonies For taking away our Charters abolishing our most valuable Laws and altering fundamentally the Forms of our Governments For suspending our own Legislatures and declaring themselves invested with power to legislate for us in all cases whatsoever He has abdicated Government here by declaring us out of his Protection and waging War against us He has plundered our seas ravaged our Coasts burnt our towns and destroyed the lives of our people He is at this time transporting large Armies of foreign Mercenaries to compleat the works of death desolation and tyranny already begun with circumstances of Cruelty & perfidy scarcely paralleled in the most barbarous ages and totally unworthy the Head of a civilized nation He has constrained our fellow Citizens taken Captive on the high Seas to bear Arms against their Country to become the executioners of their friends and Brethren or to fall themselves by their Hands He has excited domestic insurrections amongst us and has endeavoured to bring on the inhabitants of our frontiers the merciless Indian Savages whose known rule of warfare is an undistinguished destruction of all ages sexes and conditions In every stage of these Oppressions We have Petitioned for Redress in the most humble terms Our repeated Petitions have been answered only by repeated injury A Prince whose character is thus marked by every act which may define a Tyrant is unfit to be the ruler of a free people Nor have We been wanting in attentions to our Brittish brethren We have warned them from time to time of attempts by their legislature to extend an unwarrantable jurisdiction over us We have reminded them of the circumstances of our emigration and settlement here We have appealed to their native justice and magnanimity and we have conjured them by the ties of our common kindred to disavow these usurpations which would inevitably interrupt our connections and correspondence They too have been deaf to the voice of justice and of consanguinity We must therefore acquiesce in the necessity which denounces our Separation and hold them as we hold the rest of mankind Enemies in War in Peace Friends We therefore the Representatives of the united States of America in General Congress Assembled appealing to the Supreme Judge of the world for the rectitude of our intentions do in the Name and by Authority of the good People of these Colonies solemnly publish and declare That these United Colonies are and of Right ought to be Free and Independent States that they are Absolved from all Allegiance to the British Crown and that all political connection between them and the State of Great Britain is and ought to be totally dissolved and that as Free and Independent States they have full Power to levy War conclude Peace contract Alliances establish Commerce and to do all other Acts and Things which Independent States may of right do And for the support of this Declaration with a firm reliance on the protection of divine Providence we mutually pledge to each other our Lives our Fortunes and our sacred Honor".ToUpper().Split(' ');
   string[] LetterAbbreviations = "AK AL AR AZ CA CO CT DE FL GA HI ID IA IL IN KS KY LA ME MD MA MI MN MO MS MT NC ND NB NH NJ NM NV NY OH OK OR PA RI SC SD TN TX UT VA VT WA WV WI WY".Split(' ');
   string[] StateNames = "Alabama,Alaska,Arizona,Arkansas,California,Colorado,Connecticut,Delaware,Florida,Georgia,Hawaii,Idaho,Illinois,Indiana,Iowa,Kansas,Kentucky,Louisiana,Maine,Maryland,Massachusetts,Michigan,Minnesota,Mississippi,Missouri,Montana,Nebraska,Nevada,New Hampshire,New Jersey,New Mexico,New York,North Carolina,North Dakota,Ohio,Oklahoma,Oregon,Pennsylvania,Rhode Island,South Carolina,South Dakota,Tennessee,Texas,Utah,Vermont,Virginia,Washington,West Virginia,Wisconsin,Wyoming".Split(',');
   string GoalWord = "";
   string LIBRAL_SCOOL_BE_LIKE_GAY_LESON_how_to_be_be_GAYY_TRANS_LERNINNG_GAY_RECESS_CROSDRESING_HOUR_GAY_LESON_TRANGENER_LUNCH_BLM_PERIOD_COMUNIS_T_HISTORY_TAKE_NON_BINAR_BUS_HOME_THIS_ISWHAT_LEFT_WANT = "RRBRBBBBRBBRBRRRRRBBBBBRRRRBBBBBRRRRBBBRRRRRBBBRBR";
   string SubmissionWord = "";

   string[] ReallySmallStates = "Vermont,West Virginia".Split(','); //.006
   string[] SmallStates = "Alabama,Alaska,Arkansas,Georgia,Hawaii,Illinois,Indiana,Iowa,Kansas,Louisiana,Mississippi,North Carolina,Ohio,Pennsylvania,Virginia,Washington".Split(','); //.004

   bool InputMode;
   bool Focused;

   private KeyCode[] TheKeys =
  {
        //KeyCode.Backspace, KeyCode.Return,
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P,
        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M
  };
   string Qwerty = "QWERTYUIOPASDFGHJKLZXCVBNM";

   static int ModuleIdCounter = 1;
   int ModuleId;
   private bool ModuleSolved;

   void Awake () {
      ModuleId = ModuleIdCounter++;
      
      foreach (KMSelectable Button in InitalButtons) {
         Button.OnInteract += delegate () { InitialButtonPress(Button); return false; };
      }

      Module.OnFocus += delegate () { Focused = true; };
      Module.OnDefocus += delegate () { Focused = false; };

      if (Application.isEditor) {
         Focused = true;
      }
   }

   void Start () {
      //Gets a random word with length of at least 4 and without a Q
      do {
         GoalWord = DeclarationOfIndependence[Rnd.Range(0, DeclarationOfIndependence.Length)];
      } while (GoalWord.Length < 4 || GoalWord.ToUpper().Contains('Q'));

      GeneratePuzzle();
      Debug.LogFormat("[USA Cycle #{0}] The generated word is {1}.", ModuleId, GoalWord);
      string temp = "";
      for (int i = 0; i < StateIndexes.Count(); i++) {
         temp += LetterAbbreviations[StateIndexes[i]] + " ";
      }
      Debug.LogFormat("[USA Cycle #{0}] The generated (unshuffled) states are {1}.", ModuleId, temp);
      temp = "";
      /*StateIndexes.Shuffle();

      for (int i = 0; i < StateIndexes.Count(); i++) {
         temp += LetterAbbreviations[StateIndexes[i]] + " ";
      }
      Debug.LogFormat("[USA Cycle #{0}] The generated (shuffled) states are {1}.", ModuleId, temp);*/
      GetPageCount();
      DisplayStates();
   }

   void GetPageCount () {
      int StateCount = StateIndexes.Count();
      while (StateCount != 0) {
         if (StateCount / 3 != 0) {
            Pages.Add(3);
            StateCount -= 3;
         }
         else if (StateCount % 3 != 0) {
            Pages.Add(StateCount % 3);
            StateCount &= 0;
         }
      }
   }

   void GeneratePuzzle () {
      List<string> temp = new List<string> { };
      for (int i = 0; i < GoalWord.Length; i++) {
         foreach (string abbr in LetterAbbreviations) {  //For each letter, gets all possible states that could represent that letter and chooses a random one.
            if (abbr.Contains(GoalWord[i])) {
               temp.Add(abbr);
            }
         }
         StateIndexes.Add(Array.IndexOf(LetterAbbreviations, temp[Rnd.Range(0, temp.Count())]));
         temp.Clear();
      }
   }

   void InitialButtonPress (KMSelectable Button) {
      if (Button == InitalButtons[0]) {
         if (InputMode) {
            SubmissionWord = "";
         }
         else {
            PageIndex--;
            if (PageIndex < 0) {
               PageIndex = Pages.Count() - 1;
            }
            DisplayStates();
         }
      }
      else if (Button == InitalButtons[2]) {
         if (InputMode) {
            InputMode ^= true;
            SubmissionWord = "";
            DisplayStates();
         }
         else {
            PageIndex = (PageIndex + 1) % Pages.Count();
            DisplayStates();
         }
      }
      else {
         if (!InputMode) {
            InputMode = true;
            for (int i = 0; i < 3; i++) {
               ThreeStates[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 2; i++) {
               TwoStates[i].gameObject.SetActive(false);
            }
         }
         else {
            if (CheckIfWordIsValid()) {
               GetComponent<KMBombModule>().HandlePass();
               ModuleSolved = true;
               Flag.gameObject.SetActive(true);
            }
            else {
               GetComponent<KMBombModule>().HandleStrike();
            }
         }
      }
   }

   void DisplayStates () {
      switch (Pages[PageIndex]) {
         case 3:
            for (int i = 0; i < 2; i++) {
               TwoStates[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 3; i++) {
               ThreeStates[i].gameObject.SetActive(true);
               ThreeStates[i].sprite = States[StateIndexes[PageIndex * 3 + i]];
               ThreeStates[i].transform.localScale = new Vector3(ResizeStates(StateNames[StateIndexes[PageIndex * 3 + i]]), ResizeStates(StateNames[StateIndexes[PageIndex * 3 + i]]), ResizeStates(StateNames[StateIndexes[PageIndex * 3 + i]]));
               if (LIBRAL_SCOOL_BE_LIKE_GAY_LESON_how_to_be_be_GAYY_TRANS_LERNINNG_GAY_RECESS_CROSDRESING_HOUR_GAY_LESON_TRANGENER_LUNCH_BLM_PERIOD_COMUNIS_T_HISTORY_TAKE_NON_BINAR_BUS_HOME_THIS_ISWHAT_LEFT_WANT[StateIndexes[PageIndex * 3 + i]] == 'B') {
                  ThreeStates[i].color = Color.blue;
               }
               else {
                  ThreeStates[i].color = Color.red;
               }
            }
            break;
         case 2:
            for (int i = 0; i < 3; i++) {
               ThreeStates[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < 2; i++) {
               TwoStates[i].gameObject.SetActive(true);
               TwoStates[i].sprite = States[StateIndexes[PageIndex * 3 + i]];
               TwoStates[i].transform.localScale = new Vector3(ResizeStates(StateNames[StateIndexes[PageIndex * 3 + i]]), ResizeStates(StateNames[StateIndexes[PageIndex * 3 + i]]), ResizeStates(StateNames[StateIndexes[PageIndex * 3 + i]]));
               if (LIBRAL_SCOOL_BE_LIKE_GAY_LESON_how_to_be_be_GAYY_TRANS_LERNINNG_GAY_RECESS_CROSDRESING_HOUR_GAY_LESON_TRANGENER_LUNCH_BLM_PERIOD_COMUNIS_T_HISTORY_TAKE_NON_BINAR_BUS_HOME_THIS_ISWHAT_LEFT_WANT[StateIndexes[PageIndex * 3 + i]] == 'B') {
                  TwoStates[i].color = Color.blue;
               }
               else {
                  TwoStates[i].color = Color.red;
               }
            }
            break;
         default:
            for (int i = 0; i < 3; i++) {
               ThreeStates[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 2; i++) {
               TwoStates[i].gameObject.SetActive(false);
            }
            ThreeStates[1].gameObject.SetActive(true);
            ThreeStates[1].sprite = States[StateIndexes[PageIndex * 3]];
            ThreeStates[1].transform.localScale = new Vector3(ResizeStates(StateNames[StateIndexes[PageIndex * 3]]), ResizeStates(StateNames[StateIndexes[PageIndex * 3]]), ResizeStates(StateNames[StateIndexes[PageIndex * 3]]));
            if (LIBRAL_SCOOL_BE_LIKE_GAY_LESON_how_to_be_be_GAYY_TRANS_LERNINNG_GAY_RECESS_CROSDRESING_HOUR_GAY_LESON_TRANGENER_LUNCH_BLM_PERIOD_COMUNIS_T_HISTORY_TAKE_NON_BINAR_BUS_HOME_THIS_ISWHAT_LEFT_WANT[StateIndexes[PageIndex * 3]] == 'B') {
               ThreeStates[1].color = Color.blue;
            }
            else {
               ThreeStates[1].color = Color.red;
            }
            break;
      }
   }

   float ResizeStates (string Input) {
      if (Input == "Wisconsin") {
         return .001f;
      }
      else if (ReallySmallStates.Contains(Input)) {
         return .006f;
      }
      else if (SmallStates.Contains(Input)) {
         return .004f;
      }
      else if (Input == "Rhode Island") {
         return .015f;  //What the fuck rhode island
      }
      else {
         return .003f;
      }
   }

   int Modulo (int input, int By) {
      return (((input % By) + By) % By);
   }

   bool CheckIfWordIsValid () {
      if (SubmissionWord == GoalWord) {
         return true;
      }
      if (SubmissionWord.Length != GoalWord.Length || !DeclarationOfIndependence.Contains(SubmissionWord)) {
         return false;
      }
      for (int i = 0; i < SubmissionWord.Length; i++) {
         if (!LetterAbbreviations[StateIndexes[i]].Contains(SubmissionWord[i])) {
            return false;
         }
      }
      return true;
   }

   void Update () {
      if (InputMode) {
         for (int i = 0; i < TheKeys.Count(); i++) {
            if (Input.GetKeyDown(TheKeys[i]) && Focused) {
               SubmissionWord += Qwerty[i].ToString();
            }
         }
      }
      Submitting.text = SubmissionWord;
   }

#pragma warning disable 414
   private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
#pragma warning restore 414

   IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
   }

   IEnumerator TwitchHandleForcedSolve () {
      yield return null;
   }
}
