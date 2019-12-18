using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis ;

namespace MapaPolitico
{
    public partial class Form1 : Form
    {
     private System.Speech.Recognition.SpeechRecognitionEngine _recognizer = 
        new SpeechRecognitionEngine();
        private SpeechSynthesizer synth = new SpeechSynthesizer();

        public Form1() => InitializeComponent();


        private void Form1_Load(object sender, EventArgs e)
        {
            synth.Speak("Inicializando la Aplicacion");

           Grammar grammar= CreateGrammarBuilder(null);
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.UnloadAllGrammars();
            grammar.Enabled = true;
            _recognizer.LoadGrammar(grammar);
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            
            
            synth.Speak("Aplicacion preparada para reconocer su voz");

         }

     

        void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            SemanticValue semantics = e.Result.Semantics;        
            RecognitionResult result = e.Result;

            if (!semantics.ContainsKey("mapChoice"))
            {
                synth.Speak("No existe");
            }
            else
            {
                this.BackgroundImage = Image.FromFile(semantics["mapChoice"].Value.ToString());
                Update();
            }
        }


        private Grammar CreateGrammarBuilder(params int[] info) {
            synth.Speak("Creando ahora la gramatica");
            Choices mapChoice = new Choices();

            SemanticResultValue choiceResultValue =
                new SemanticResultValue("Comunidad Valenciana", MapaPolitico.Properties.Resources.ComunidadValenciana);
            GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Andalucia", MapaPolitico.Properties.Resources.Andalucia);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Aragon", MapaPolitico.Properties.Resources.Aragon);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Asturias", MapaPolitico.Properties.Resources.Asturias);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Baleares", MapaPolitico.Properties.Resources.Baleares);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Canarias", MapaPolitico.Properties.Resources.Canarias);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Cantabria", MapaPolitico.Properties.Resources.Cantabria);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Castilla la Mancha", MapaPolitico.Properties.Resources.CastillaLaMancha);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Castilla leon", MapaPolitico.Properties.Resources.CastillaLeon);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Catalunya", MapaPolitico.Properties.Resources.Catalunya);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Extremadura", MapaPolitico.Properties.Resources.Extremadura);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Galicia", MapaPolitico.Properties.Resources.Galicia);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("La Rioja", MapaPolitico.Properties.Resources.LaRioja);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Madrid", MapaPolitico.Properties.Resources.Madrid);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Murcia", MapaPolitico.Properties.Resources.Murcia);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Navarra", MapaPolitico.Properties.Resources.Navarra);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            choiceResultValue =
                new SemanticResultValue("Pais Vasco", MapaPolitico.Properties.Resources.PaisVasco);
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            mapChoice.Add(resultValueBuilder);

            SemanticResultKey choiceResultKey = new SemanticResultKey("map", mapChoice);
            GrammarBuilder mapas = new GrammarBuilder(choiceResultKey);

            GrammarBuilder pintar = "Pintar";
            GrammarBuilder colorear = "Colorear";

            Choices dos_alternativas = new Choices(pintar, colorear);
            GrammarBuilder frase = new GrammarBuilder(dos_alternativas);
            Grammar grammar = new Grammar(frase);
            grammar.Name = "Pintar/Colorear";

            return grammar;

        }

    }
}