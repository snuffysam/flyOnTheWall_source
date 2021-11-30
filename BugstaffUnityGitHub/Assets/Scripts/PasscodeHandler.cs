using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Platformer.Mechanics;
using UnityEngine.SceneManagement;

public class PasscodeHandler : MonoBehaviour
{
    public static List<string> passcodes;
    public static bool hardcore;

    public static string[] heartNames = {"HeartIntro2", "HeartPalace2", "HeartAirship3", "HeartCasino4", "HeartPalmChase"};
    // Start is called before the first frame update
    void Start()
    {
        if (passcodes == null){
            hardcore = false;
        }
        SetUpPasscodes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string GetCurrentPasscode(){
        if (passcodes == null){
            SetUpPasscodes();
        }
        if (hardcore){
            return "N/A";
        }

        bool[] parameters = new bool[9];
        parameters[0] = Health.HasHeart(heartNames[0]);
        //Debug.Log("intro heart : " + parameters[0]);
        parameters[1] = Health.HasHeart(heartNames[1]);
        //Debug.Log("palace heart : " + parameters[1]);
        parameters[2] = Health.HasHeart(heartNames[2]);
        //Debug.Log("airship heart : " + parameters[2]);
        parameters[3] = Health.HasHeart(heartNames[3]);
        //Debug.Log("casino heart : " + parameters[3]);
        parameters[4] = Health.HasHeart(heartNames[4]);
        //Debug.Log("palm heart : " + parameters[4]);
        parameters[5] = MissionManager.IsMissionBeaten(0);
        //Debug.Log("beat palace : " + parameters[5]);
        parameters[6] = MissionManager.IsMissionBeaten(1);
        //Debug.Log("beat airship : " + parameters[6]);
        parameters[7] = MissionManager.IsMissionBeaten(2);
        //Debug.Log("beat casino : " + parameters[7]);
        parameters[8] = MissionManager.beatPalmMission; //palm section beaten
        //Debug.Log("beat palm : " + parameters[8]);

        int index = 0;

        string bitString = "";
        for (int i = 0; i < parameters.Length; i++){
            if (parameters[i]){
                index += (int)(Mathf.Pow(2,i));
                bitString = bitString + "1";
            } else {
                bitString = bitString + "0";
            }
        }

        Debug.Log("current passcode - " + index + " - " + passcodes[index] + " - " + bitString);

        return passcodes[index];
    }

    public static bool LoadFromPasscode(string code){
        if (code.Equals("JUSTINBAILEY")){
            Health.ResetHearts();
            PlayerController.AllPowerups();
            //set up hardcore mode
            hardcore = true;
            SceneManager.LoadScene("Intro1");
            return true;
        }
        hardcore = false;
        if (!passcodes.Contains(code)){
            return false;
        }
        PlayerController.ResetPowerups();
        Health.ResetHearts();
        BitVector32 bits = new BitVector32(passcodes.IndexOf(code));
        Debug.Log("bits for: " + passcodes.IndexOf(code) + " - " + code + " - " + bits);
        string bitString = "" + bits;
        int bitsLength = bitString.Length;

        if (bitString[bitsLength-2] == '1'){
            Debug.Log("intro heart");
            Health.AddHeart(heartNames[0]);
        }
        if (bitString[bitsLength-3] == '1'){
            Debug.Log("palace heart");
            Health.AddHeart(heartNames[1]);
        }
        if (bitString[bitsLength-4] == '1'){
            Debug.Log("airship heart");
            Health.AddHeart(heartNames[2]);
        }
        if (bitString[bitsLength-5] == '1'){
            Debug.Log("casino heart");
            Health.AddHeart(heartNames[3]);
        }
        if (bitString[bitsLength-6] == '1'){
            Debug.Log("palm heart");
            Health.AddHeart(heartNames[4]);
        }
        if (bitString[bitsLength-7] == '1'){
            Debug.Log("beat palace");
            MissionManager.missionIndex = 0;
            MissionManager.UpdatePlayerGadgets();
            MissionManager.CompleteMission();
        }
        if (bitString[bitsLength-8] == '1'){
            Debug.Log("beat airship");
            MissionManager.missionIndex = 1;
            MissionManager.UpdatePlayerGadgets();
            MissionManager.CompleteMission();
        }
        if (bitString[bitsLength-9] == '1'){
            Debug.Log("beat casino");
            MissionManager.missionIndex = 2;
            MissionManager.UpdatePlayerGadgets();
            MissionManager.CompleteMission();
        }
        if (bitString[bitsLength-10] == '1'){
            Debug.Log("beat palm");
            MissionManager.missionIndex = 3;
            MissionManager.UpdatePlayerGadgets();
            SceneManager.LoadScene("Finale1");
            //do stuff for palm mission
        } else {
            SceneManager.LoadScene("MissionSelect");
        }
        return true;
    }

    static void SetUpPasscodes(){
        if (passcodes != null){
            return;
        }

        passcodes = new List<string>();
        passcodes.Add("SUBSOLAR");
        passcodes.Add("VANDELAS");
        passcodes.Add("KILLCOW");
        passcodes.Add("OSMATIC");
        passcodes.Add("AGGER");
        passcodes.Add("NEOMORPHIC");
        passcodes.Add("KEMP");
        passcodes.Add("WACK");
        passcodes.Add("CORACOID");
        passcodes.Add("EXEMPLUM");
        passcodes.Add("EREMOPHYTE");
        passcodes.Add("HEARTSOME");
        passcodes.Add("UNZYMOTIC");
        passcodes.Add("FABULOUS");
        passcodes.Add("TOOTLE");
        passcodes.Add("SOCIATIVE");
        passcodes.Add("MONOTROCH");
        passcodes.Add("VELOMETER");
        passcodes.Add("UNIVOROUS");
        passcodes.Add("SACRALGIA");
        passcodes.Add("TOROID");
        passcodes.Add("PHAGOMANIA");
        passcodes.Add("SUDORIFIC");
        passcodes.Add("IZARD");
        passcodes.Add("JUNCO");
        passcodes.Add("WASHBOARD");
        passcodes.Add("GRANDINOUS");
        passcodes.Add("ABSOLUTORY");
        passcodes.Add("ACARICIDE");
        passcodes.Add("SIRIASIS");
        passcodes.Add("SUNSTROKE");
        passcodes.Add("VEGANIC");
        passcodes.Add("CERGE");
        passcodes.Add("ISOPLETH");
        passcodes.Add("ROCHE");
        passcodes.Add("ALVEOLATE");
        passcodes.Add("SPECIOCIDE");
        passcodes.Add("QUICKSET");
        passcodes.Add("MANICATE");
        passcodes.Add("MONOLITH");
        passcodes.Add("PEDIGEROUS");
        passcodes.Add("HEPHAESTIC");
        passcodes.Add("GAMBADO");
        passcodes.Add("CONSTRINGE");
        passcodes.Add("LEXIS");
        passcodes.Add("THREPTIC");
        passcodes.Add("MURRHINE");
        passcodes.Add("ARANEIDAN");
        passcodes.Add("DENTIFORM");
        passcodes.Add("TONTINE");
        passcodes.Add("CAGAMOSIS");
        passcodes.Add("OENOPHILE");
        passcodes.Add("COSMORAMA");
        passcodes.Add("LAPPACEOUS");
        passcodes.Add("PRICKLY");
        passcodes.Add("BERLIN");
        passcodes.Add("TANTIVY");
        passcodes.Add("DRAGONISM");
        passcodes.Add("ZORI");
        passcodes.Add("KYLOE");
        passcodes.Add("GLAREOUS");
        passcodes.Add("PERRUQUIER");
        passcodes.Add("WIGMAKER");
        passcodes.Add("VELOUTINE");
        passcodes.Add("PARRHESIA");
        passcodes.Add("MUSCICIDE");
        passcodes.Add("ERETHISM");
        passcodes.Add("RATHE");
        passcodes.Add("HYGIASTICS");
        passcodes.Add("OLIGOPSONY");
        passcodes.Add("PERISTYLE");
        passcodes.Add("GNOSTICISM");
        passcodes.Add("CONJURER");
        passcodes.Add("PSOAS");
        passcodes.Add("TIMENOGUY");
        passcodes.Add("MULLER");
        passcodes.Add("PUNCTULATE");
        passcodes.Add("CONFUTE");
        passcodes.Add("NODOSE");
        passcodes.Add("ABRADANT");
        passcodes.Add("GINNEL");
        passcodes.Add("LAGAN");
        passcodes.Add("MONANTHOUS");
        passcodes.Add("SKERRY");
        passcodes.Add("UGHTEN");
        passcodes.Add("REFECTION");
        passcodes.Add("CACONYM");
        passcodes.Add("PLENITUDE");
        passcodes.Add("PREBEND");
        passcodes.Add("RHOTACISM");
        passcodes.Add("MADAROSIS");
        passcodes.Add("GENET");
        passcodes.Add("PROFULGENT");
        passcodes.Add("AUTOGRAPHY");
        passcodes.Add("ROTA");
        passcodes.Add("ROSTER");
        passcodes.Add("VENAL");
        passcodes.Add("ATRAMENT");
        passcodes.Add("YAWL");
        passcodes.Add("CWM");
        passcodes.Add("WIDGEON");
        passcodes.Add("COQUILLAGE");
        passcodes.Add("WIREDRAW");
        passcodes.Add("QUANT");
        passcodes.Add("VIRGA");
        passcodes.Add("TALLIATE");
        passcodes.Add("SKINTLE");
        passcodes.Add("ABJECT");
        passcodes.Add("PARAPHRAST");
        passcodes.Add("APOPEMPTIC");
        passcodes.Add("ECBOLE");
        passcodes.Add("DIGRESSION");
        passcodes.Add("PARABOLA");
        passcodes.Add("VIRGUNCULE");
        passcodes.Add("VIRELAY");
        passcodes.Add("PULVIL");
        passcodes.Add("PRATINCOLE");
        passcodes.Add("ALMUCE");
        passcodes.Add("VISIONIC");
        passcodes.Add("PYROGNOMIC");
        passcodes.Add("ADENIA");
        passcodes.Add("ONOMANCY");
        passcodes.Add("CNEMIS");
        passcodes.Add("JINKER");
        passcodes.Add("CONTICENT");
        passcodes.Add("SILENT");
        passcodes.Add("PRELECT");
        passcodes.Add("RADULAR");
        passcodes.Add("SAPID");
        passcodes.Add("LOGOGRAM");
        passcodes.Add("VITRAGE");
        passcodes.Add("SMALT");
        passcodes.Add("CUBIFORM");
        passcodes.Add("HOGGASTER");
        passcodes.Add("RADIOGENIC");
        passcodes.Add("ARCHITRAVE");
        passcodes.Add("STYLOBATE");
        passcodes.Add("GABLOCK");
        passcodes.Add("TESTAMUR");
        passcodes.Add("ETHNOCRACY");
        passcodes.Add("STOMACHER");
        passcodes.Add("SIGNIFICS");
        passcodes.Add("ABUTMENT");
        passcodes.Add("ANGSTROM");
        passcodes.Add("SOLIPSISM");
        passcodes.Add("SINAL");
        passcodes.Add("VIBRATILE");
        passcodes.Add("ZOOTAXY");
        passcodes.Add("ZOOMANIA");
        passcodes.Add("FAVEOLATE");
        passcodes.Add("PLEXUS");
        passcodes.Add("NETWORK");
        passcodes.Add("VINEATIC");
        passcodes.Add("AESTHESIA");
        passcodes.Add("XANTHOPSIA");
        passcodes.Add("NITRIARY");
        passcodes.Add("DRAFFISH");
        passcodes.Add("WORTHLESS");
        passcodes.Add("WHIPSTALL");
        passcodes.Add("BANTLING");
        passcodes.Add("DEMESNE");
        passcodes.Add("ATOKOUS");
        passcodes.Add("PLODGE");
        passcodes.Add("ALKANET");
        passcodes.Add("ZABERNISM");
        passcodes.Add("DUNNART");
        passcodes.Add("PHOCINE");
        passcodes.Add("DIOPTRICS");
        passcodes.Add("TIPPET");
        passcodes.Add("LIBRATION");
        passcodes.Add("APHESIS");
        passcodes.Add("LITEROSE");
        passcodes.Add("WINDTHROW");
        passcodes.Add("INTROMIT");
        passcodes.Add("PRECONCERT");
        passcodes.Add("PERSEITY");
        passcodes.Add("GENERALATE");
        passcodes.Add("HENRY");
        passcodes.Add("CALENTURE");
        passcodes.Add("ALABAMINE");
        passcodes.Add("SIEGE");
        passcodes.Add("FATIDICAL");
        passcodes.Add("EFFLEURAGE");
        passcodes.Add("SWANSKIN");
        passcodes.Add("HAVER");
        passcodes.Add("JALOUSIE");
        passcodes.Add("HEELER");
        passcodes.Add("OSSIFY");
        passcodes.Add("LEMURINE");
        passcodes.Add("EMBOWER");
        passcodes.Add("EXORDIUM");
        passcodes.Add("CREWEL");
        passcodes.Add("STARBOLINS");
        passcodes.Add("CATHEAD");
        passcodes.Add("LIMIVOROUS");
        passcodes.Add("COETANEOUS");
        passcodes.Add("LENIFY");
        passcodes.Add("TORVOUS");
        passcodes.Add("STERN");
        passcodes.Add("ABODEMENT");
        passcodes.Add("TRADUCTIVE");
        passcodes.Add("HOMOOUSIA");
        passcodes.Add("STEGNOSIS");
        passcodes.Add("JOUGS");
        passcodes.Add("PORTIUNCLE");
        passcodes.Add("TELESIS");
        passcodes.Add("TRIBOMETER");
        passcodes.Add("PETROUS");
        passcodes.Add("STONY");
        passcodes.Add("VESPOID");
        passcodes.Add("SUPINATE");
        passcodes.Add("CLEVIS");
        passcodes.Add("LAXATIVE");
        passcodes.Add("DOUCET");
        passcodes.Add("COELOM");
        passcodes.Add("POSTULATOR");
        passcodes.Add("SUBTEND");
        passcodes.Add("UROLOGY");
        passcodes.Add("SURQUEDRY");
        passcodes.Add("ARROGANCE");
        passcodes.Add("TALIGRADE");
        passcodes.Add("ACROPATHY");
        passcodes.Add("ARRENDATOR");
        passcodes.Add("VIGORO");
        passcodes.Add("VOLARY");
        passcodes.Add("AVIARY");
        passcodes.Add("KERN");
        passcodes.Add("RHATHYMIA");
        passcodes.Add("PISCICIDE");
        passcodes.Add("DAMINE");
        passcodes.Add("TWAIN");
        passcodes.Add("TWO");
        passcodes.Add("RUSSET");
        passcodes.Add("ACOCK");
        passcodes.Add("DEFIANTLY");
        passcodes.Add("KERYGMATIC");
        passcodes.Add("SEDULOUS");
        passcodes.Add("ADEMPTION");
        passcodes.Add("MANSWORN");
        passcodes.Add("PERJURED");
        passcodes.Add("MORIENT");
        passcodes.Add("DYING");
        passcodes.Add("VARIOMETER");
        passcodes.Add("REVETMENT");
        passcodes.Add("VARDO");
        passcodes.Add("DORNICK");
        passcodes.Add("CLAMANCY");
        passcodes.Add("URGENCY");
        passcodes.Add("CHATON");
        passcodes.Add("VIOLACEOUS");
        passcodes.Add("BIOMETRICS");
        passcodes.Add("XERANSIS");
        passcodes.Add("SANBENITO");
        passcodes.Add("GONIOMETER");
        passcodes.Add("CONTECT");
        passcodes.Add("VAGILITY");
        passcodes.Add("SYNEIDESIS");
        passcodes.Add("SPELTER");
        passcodes.Add("ZINC");
        passcodes.Add("HAMBO");
        passcodes.Add("CRESSET");
        passcodes.Add("ZOOGENIC");
        passcodes.Add("GAMBREL");
        passcodes.Add("ACROLOGIC");
        passcodes.Add("MARMAROSIS");
        passcodes.Add("SHABRACK");
        passcodes.Add("PUCELLE");
        passcodes.Add("FESS");
        passcodes.Add("OBVERT");
        passcodes.Add("PAROMOION");
        passcodes.Add("SLEY");
        passcodes.Add("SHALLOT");
        passcodes.Add("CTENIFORM");
        passcodes.Add("DESINENT");
        passcodes.Add("OLITORY");
        passcodes.Add("XANTHODERM");
        passcodes.Add("ASPERITY");
        passcodes.Add("CAPARISON");
        passcodes.Add("NACELLE");
        passcodes.Add("MAZURKA");
        passcodes.Add("DUCDAME");
        passcodes.Add("PAPETERIE");
        passcodes.Add("ERGOGRAPH");
        passcodes.Add("JECORAL");
        passcodes.Add("NEUSTON");
        passcodes.Add("ZEP");
        passcodes.Add("PEMBROKE");
        passcodes.Add("ASCETICISM");
        passcodes.Add("SPINTRIAN");
        passcodes.Add("COSMOGENIC");
        passcodes.Add("FAMIGERATE");
        passcodes.Add("AURILAVE");
        passcodes.Add("NOMISM");
        passcodes.Add("INDICIA");
        passcodes.Add("VAUCLUSIAN");
        passcodes.Add("NIMIETY");
        passcodes.Add("EXCESS");
        passcodes.Add("ACROTISM");
        passcodes.Add("TOROSE");
        passcodes.Add("SETULOUS");
        passcodes.Add("PROTACTIC");
        passcodes.Add("BOUN");
        passcodes.Add("ILLOCUTION");
        passcodes.Add("ATTRIST");
        passcodes.Add("VERMIAN");
        passcodes.Add("LAIRWITE");
        passcodes.Add("LOUCHE");
        passcodes.Add("HOGGIN");
        passcodes.Add("MARTINGALE");
        passcodes.Add("INUMBRATE");
        passcodes.Add("WAKERIFE");
        passcodes.Add("LEMMA");
        passcodes.Add("SPONGOLOGY");
        passcodes.Add("GYRON");
        passcodes.Add("TEUTHOLOGY");
        passcodes.Add("CERUMEN");
        passcodes.Add("CURETTE");
        passcodes.Add("HACKLE");
        passcodes.Add("SLOYD");
        passcodes.Add("THEFTUOUS");
        passcodes.Add("THIEVISH");
        passcodes.Add("TIGERISM");
        passcodes.Add("SWAGGER");
        passcodes.Add("LOGOGOGUE");
        passcodes.Add("CORDATE");
        passcodes.Add("CANTATRICE");
        passcodes.Add("JUBILATE");
        passcodes.Add("SQUALIFORM");
        passcodes.Add("SOPHISM");
        passcodes.Add("MUMPSIMUS");
        passcodes.Add("FALDERAL");
        passcodes.Add("CENTILOQUY");
        passcodes.Add("CHAPLET");
        passcodes.Add("AGNIZE");
        passcodes.Add("GRAVEOLENT");
        passcodes.Add("EUONYM");
        passcodes.Add("INKHORNISM");
        passcodes.Add("QUILOMBO");
        passcodes.Add("DALTON");
        passcodes.Add("PARAPH");
        passcodes.Add("PYCNOSIS");
        passcodes.Add("THICKENING");
        passcodes.Add("VISCOMETER");
        passcodes.Add("HYGIOLOGY");
        passcodes.Add("PALILLOGY");
        passcodes.Add("HOMOTAXIC");
        passcodes.Add("CLEDONISM");
        passcodes.Add("GREAVE");
        passcodes.Add("HANAP");
        passcodes.Add("TOG");
        passcodes.Add("CANCRIZANS");
        passcodes.Add("LOGICASTER");
        passcodes.Add("IVI");
        passcodes.Add("PAYNIMRY");
        passcodes.Add("HEATHENDOM");
        passcodes.Add("EROMANCY");
        passcodes.Add("OTACOUSTIC");
        passcodes.Add("KIDOLOGY");
        passcodes.Add("LEPROSE");
        passcodes.Add("PHOTOPHILE");
        passcodes.Add("SAPWOOD");
        passcodes.Add("BRIO");
        passcodes.Add("EVERSION");
        passcodes.Add("PARANYMPH");
        passcodes.Add("PLANIGRAPH");
        passcodes.Add("SPHENOID");
        passcodes.Add("IRENICON");
        passcodes.Add("SETLINE");
        passcodes.Add("SEROTINOUS");
        passcodes.Add("KENOSIS");
        passcodes.Add("BELDAM");
        passcodes.Add("PHATIC");
        passcodes.Add("STASIARCH");
        passcodes.Add("YEN");
        passcodes.Add("ORICHALC");
        passcodes.Add("PHRENESIS");
        passcodes.Add("RATH");
        passcodes.Add("GUILLEMET");
        passcodes.Add("URDÉ");
        passcodes.Add("OPHIC");
        passcodes.Add("TELENERGY");
        passcodes.Add("MONOCULUS");
        passcodes.Add("MOMILOGY");
        passcodes.Add("HYETOLOGY");
        passcodes.Add("MYCOSIS");
        passcodes.Add("AUDISM");
        passcodes.Add("ALOMANCY");
        passcodes.Add("YERK");
        passcodes.Add("INSIPIENCE");
        passcodes.Add("PHOTISM");
        passcodes.Add("PERLATIVE");
        passcodes.Add("REVERS");
        passcodes.Add("EPEOLATRY");
        passcodes.Add("SURETYSHIP");
        passcodes.Add("CROQUINOLE");
        passcodes.Add("ITERANT");
        passcodes.Add("SORTATION");
        passcodes.Add("JUGATE");
        passcodes.Add("GROGGERY");
        passcodes.Add("OPHIOLOGY");
        passcodes.Add("WHIFFET");
        passcodes.Add("ANTRORSE");
        passcodes.Add("CURLEW");
        passcodes.Add("CABRIE");
        passcodes.Add("PERIPTERAL");
        passcodes.Add("SPATIALISM");
        passcodes.Add("ABITURIENT");
        passcodes.Add("YONI");
        passcodes.Add("VATICINY");
        passcodes.Add("OBI");
        passcodes.Add("PISTOLOGY");
        passcodes.Add("MAINMAST");
        passcodes.Add("ARIGHT");
        passcodes.Add("COFFRET");
        passcodes.Add("MONGERY");
        passcodes.Add("TYROID");
        passcodes.Add("RASCHEL");
        passcodes.Add("ORDALIAN");
        passcodes.Add("TURTLEBACK");
        passcodes.Add("REQUIESCAT");
        passcodes.Add("SHEADING");
        passcodes.Add("POLYERGIC");
        passcodes.Add("ENERGISM");
        passcodes.Add("HEDERA");
        passcodes.Add("PRECATIVE");
        passcodes.Add("PLECTRUM");
        passcodes.Add("BIOGENESIS");
        passcodes.Add("VENTIFACT");
        passcodes.Add("TWIFORKED");
        passcodes.Add("BIFURCATE");
        passcodes.Add("MOTTE");
        passcodes.Add("SAGENE");
        passcodes.Add("TRUCULENT");
        passcodes.Add("QUIRITARY");
        passcodes.Add("SKEG");
        passcodes.Add("TAURINE");
        passcodes.Add("PARACUSIS");
        passcodes.Add("PARDONER");
        passcodes.Add("SCATCH");
        passcodes.Add("URCEOLATE");
        passcodes.Add("NOCTUARY");
        passcodes.Add("ORAD");
        passcodes.Add("PROROGUE");
        passcodes.Add("VISCACHA");
        passcodes.Add("SKIAMACHY");
        passcodes.Add("POSIGRADE");
        passcodes.Add("VERNISSAGE");
        passcodes.Add("ATEKNIA");
        passcodes.Add("CATHOLICOS");
        passcodes.Add("EMMENOLOGY");
        passcodes.Add("SLAVOPHILE");
        passcodes.Add("PARURE");
        passcodes.Add("KONIMETER");
        passcodes.Add("LANGLAUF");
        passcodes.Add("WIMPLE");
        passcodes.Add("POLEMIC");
        passcodes.Add("BARBULA");
        passcodes.Add("WEBWHEEL");
        passcodes.Add("MATELASSÉ");
        passcodes.Add("EIKONOLOGY");
        passcodes.Add("METAPHOR");
        passcodes.Add("WYRD");
        passcodes.Add("ALGOLAGNIA");
        passcodes.Add("CYLLOSIS");
        passcodes.Add("FITCHEW");
        passcodes.Add("POLECAT");
        passcodes.Add("GROUNDLING");
        passcodes.Add("URBANOLOGY");
        passcodes.Add("PLANGENCY");
        passcodes.Add("FRUCTUARY");
        passcodes.Add("SUBTRUDE");
        passcodes.Add("CLAUDENT");
        passcodes.Add("RADIOSCOPE");
        passcodes.Add("JESSAMY");
        passcodes.Add("CRUMENAL");
        passcodes.Add("AURIGATION");
        passcodes.Add("YOICKS");
        passcodes.Add("SPELEOTHEM");
        passcodes.Add("REDOUBT");
        passcodes.Add("SFERICS");
        passcodes.Add("UVALA");
        passcodes.Add("INSPAN");
        passcodes.Add("VARA");
        passcodes.Add("APATETIC");
        passcodes.Add("ANACARDIC");
        passcodes.Add("SCALPTURE");
        passcodes.Add("UNIPHONOUS");
        passcodes.Add("AUTOCRACY");
        passcodes.Add("JUVENAL");
        passcodes.Add("SARSENET");
        passcodes.Add("DEFERRISE");
        passcodes.Add("IMMURE");
        passcodes.Add("NEBULE");
        passcodes.Add("POIMENIC");
        passcodes.Add("PASTORAL");
        passcodes.Add("LUCIFORM");
        passcodes.Add("PARESIS");
        passcodes.Add("CHAOLOGY");
        passcodes.Add("TERSIVE");
        passcodes.Add("ABIGEUS");
        passcodes.Add("PULMONIC");
        passcodes.Add("FLAMEN");
        passcodes.Add("STROBIC");
        passcodes.Add("APSIDAL");
        passcodes.Add("QAT");
        passcodes.Add("DASYURE");
        passcodes.Add("PALZOGONY");
        passcodes.Add("WAYMENT");
        passcodes.Add("THERM");
        passcodes.Add("LUXURIANT");
        passcodes.Add("SABLIERE");
        passcodes.Add("TONSURE");

        for (int i = 0; i < passcodes.Count; i++){
            passcodes[i] = passcodes[i].ToUpper();
            for (int k = 0; k < i; k++){
                if (passcodes[i].Equals(passcodes[k])){
                    Debug.Log("duplicate found! " + passcodes[k]);
                }
            }
        }
    }
}
