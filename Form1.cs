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
        
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            synth.Speak("Inicializando la Aplicacion");

           Grammar grammar= CreateGrammarBuilder(null);
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.UnloadAllGrammars();
            grammar.Enabled = true;
            _recognizer.LoadGrammar(grammar);
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
            //reconocimiento asincrono y mutiples veces
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            
          // this.BackgroundImage = MapaPolitico.Properties.Resources.mapaBlanco;
            
            synth.Speak("Aplicación preparada para reconocer su voz");

         }

     

        void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
                      //obtenemos un diccionario con los elementos sematicos
                      SemanticValue semantics = e.Result.Semantics;
          
                      string rawText = e.Result.Text;
                      RecognitionResult result = e.Result;

                      if (!semantics.ContainsKey("rgb"))
                      {
                          this.label1.Text = "No info provided.";
                      }
                      else
                      {
                          this.label1.Text = rawText;
                          this.BackColor = Color.FromArgb((int)semantics["rgb"].Value);
                          Update();
                          synth.Speak(rawText);
                      }
        }


        private Grammar CreateGrammarBuilder(params int[] info) {
            synth.Speak("Creando ahora la gramatica");
            Choices mapChoice = new Choices();

            SemanticResultValue choiceResultValue =
                new SemanticResultValue("Comunidad Valenciana", MapaPolitico.Properties.Resources.ComunidadValenciana);
            GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
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
      
      /*  private Grammar CreateGrammarBuilderRGBSemantics2(params int[] info)
        {
            synth.Speak("Creando ahora la gramática");
            Choices colorChoice = new Choices();
   
            SemanticResultValue choiceResultValue =
                    new SemanticResultValue("Rojo", Color.FromName("Red").ToArgb());
            GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
            colorChoice.Add(resultValueBuilder);
            
            choiceResultValue =
                   new SemanticResultValue("Azul", Color.FromName("Blue").ToArgb());
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            colorChoice.Add(resultValueBuilder);
            
            choiceResultValue =
                   new SemanticResultValue("Verde", Color.FromName("Green").ToArgb());
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            colorChoice.Add(resultValueBuilder);

            SemanticResultKey choiceResultKey = new SemanticResultKey("rgb", colorChoice);
            GrammarBuilder colores = new GrammarBuilder(choiceResultKey);


            GrammarBuilder poner= "Poner";
            GrammarBuilder cambiar ="Cambiar";
            GrammarBuilder fondo = "Fondo";

            Choices dos_alternativas = new Choices(poner, cambiar);
            GrammarBuilder frase = new GrammarBuilder(dos_alternativas);
            frase.Append(fondo);
            frase.Append(colores);
            Grammar grammar = new Grammar(frase);
            grammar.Name = "Poner/Cambiar Fondo";
            return grammar;


       
        }*/

    }
}