using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO.Ports;
using System.IO;




namespace Voice_recognition
{
    

    public partial class Form1 : Form
    {
        SpeechRecognitionEngine spRec = new SpeechRecognitionEngine();
        SpeechSynthesizer spSynt = new SpeechSynthesizer();
        SerialPort myPort = new SerialPort("COM5", 115200);
        int i;
        int f = 0, k=0;
        int a, b;
        int c = 0;
        int s = 0, d = 0, m=0;
        int add, sub, mul;
        string z;
       

        string[] number = new string[100];
        

        public Form1()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            spRec.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;
           // myPort.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            for (i = 0; i < 100; i++)
            {
                string z = string.Join(",", i);
                number[i] = z;
            }
           
                commands.Add(new string[] {"them","guide", "time","read", "naam kya hey?","nimo", "print my name","a plus b", "a into b","a minus b", "move forward", "move backward", "move right", "move left", "stop", " LED ON","LED OFF" });
           
           
                commands.Add(number);
           
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            spRec.LoadGrammarAsync(grammar);
            spRec.SetInputToDefaultAudioDevice();
            spRec.SpeechRecognized += recEngine_SpeechRecognized;
            
        }
        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
           
            if (e.Result.Text != "" )
            {
               if(f==1)
                {
                    c++;
                    f = 0;
                }
                if (c==1 && f==0)
                {
                    
                    a = Convert.ToInt32(e.Result.Text);
                    richTextBox1.Text += "\n";
                    richTextBox1.Text += (a);
                    spSynt.SpeakAsync("provide 2nd number");
                    f = 1;
                  

                }

                else if (c == 2 && f==0)
                {
                    
                    b = Convert.ToInt32(e.Result.Text);
                    richTextBox1.Text += "\n";
                    richTextBox1.Text += (b);
                    if (s == 1)
                    {
                        add = a + b;
                        string h = string.Join(",", add);
                        richTextBox1.Text += "\nAnswer = ";
                        richTextBox1.Text += (add);
                        spSynt.SpeakAsync("answer equals");
                        spSynt.SpeakAsync(h);
                        s = 0;
                    }
                    else if (m == 1)
                    {
                        mul = a * b;
                        string h = string.Join(",", mul);
                        richTextBox1.Text += "\nAnswer = ";
                        richTextBox1.Text += (mul);
                        spSynt.SpeakAsync("answer equals");
                        spSynt.SpeakAsync(h);
                        m = 0;
                    }
                    else if (d == 1)
                    {
                        sub = a - b;
                        string h = string.Join(",", sub);
                        richTextBox1.Text += "\nAnswer = ";
                        richTextBox1.Text += (sub);
                        spSynt.SpeakAsync("answer equals");
                        spSynt.SpeakAsync(h);
                        d = 0;
                    }
                    c = 0;

                }
            }
           
            switch (e.Result.Text)
            {
                case "them":
                    richTextBox1.Text = "\n" + "Thank you for watching";

                    spSynt.SpeakAsync("Thank you for Watching");

                    break;

                case "read":
                    char[] ch0 = { 'R' };
                    myPort.Write(ch0, 0, 1);
                    String line = "";
                    while (myPort.BytesToRead ==0)
                    {}
                    while (myPort.BytesToRead > 0)
                    {
                        line += (char)myPort.ReadChar();
                    }
                   
                    richTextBox1.Text += "\n";
                    richTextBox1.Text += line;
                    spSynt.SpeakAsync(line);

                    break;

                case "guide":
                    richTextBox1.Text += "\n" + "guide";
                                       
                    spSynt.SpeakAsync("guide");
                    while (true)
                    {
                        char l;
                        l = (char)myPort.ReadChar();
                        if (l == 'o')
                        {
                            richTextBox1.Text += "\n";
                            richTextBox1.Text += "human in front";

                            spSynt.SpeakAsync("human in front");
                        }
                        
                    }
                    break;
                
                case "naam kya hey?":
                    richTextBox1.Text += "\nmera naam Nimo hey!";

                    spSynt.SpeakAsync("mera naam, Nimo hey!");

                    break;

                case "whose your creator":
                    richTextBox1.Text += "\nAkash Jadhav";

                    spSynt.SpeakAsync("Akash Jadhav!");

                    break;

                case "nimo":
                    richTextBox1.Text += "\nHow may I help you?";
                   
                    spSynt.SpeakAsync("How may I help you?");

                    break;

                case "time":
                    richTextBox1.Text = "\n" + DateTime.Now.ToShortTimeString();

                    spSynt.SpeakAsync(DateTime.Now.ToShortTimeString());

                    break;

                case "hello nimo":
                    richTextBox1.Text += "\nHello buddy! How are you?";
                   
                    spSynt.SpeakAsync("Hello buddy. How are you?");
                   
                    break;

               
                case "print my name":
                    richTextBox1.Text += "\nAkash";
                   
                    break;

                case "a plus b":
                    richTextBox1.Text ="\n" + "addition";
                    spSynt.SpeakAsync("provide 1st number");
                    f = 1;
                    s = 1;

                    break;

                case "a into b":
                    richTextBox1.Text ="\n" + "multiplication";
                    spSynt.SpeakAsync("provide 1st number");
                    f = 1;
                    m = 1;

                    break;

                case "a minus b":
                    richTextBox1.Text = "\n" + "subtraction";
                    spSynt.SpeakAsync("provide 1st number");
                    f = 1;
                    d = 1;

                    break;

                case "move forward":
                    richTextBox1.Text += "\n";
                    richTextBox1.Text += "forward";
                    spSynt.SpeakAsync("moving forward");
                    char[] ch2 = { 'f' };
                    myPort.Write(ch2, 0, 1);

                    break;

                case "move backward":
                    richTextBox1.Text += "\n";
                    richTextBox1.Text += "backward";
                    spSynt.SpeakAsync("moving backward");
                    char[] ch3 = { 'b' };
                    myPort.Write(ch3, 0, 1);

                    break;

                case "move right":
                    richTextBox1.Text += "\n";
                    richTextBox1.Text += "right";
                    spSynt.SpeakAsync("moving right");
                    char[] ch4 = { 'r' };
                    myPort.Write(ch4, 0, 1);

                    break;

                case "move left":
                    richTextBox1.Text += "\n";
                    richTextBox1.Text += "left";
                    spSynt.SpeakAsync("moving left");
                    char[] ch5 = { 'l' };
                    myPort.Write(ch5, 0, 1);
                    break;

                case "stop":
                    richTextBox1.Text += "\n";
                    richTextBox1.Text += "stop";
                    spSynt.SpeakAsync("stopping");
                    char[] ch6 = { 's' };
                    myPort.Write(ch6, 0, 1);
                    break;

                case "LED ON":
                    richTextBox1.Text ="\n" + "turned on";
                    spSynt.SpeakAsync("turned on");
                    char[] ch = { 'a' };
                    myPort.Write(ch, 0, 1);
                   
                    break;

                case "LED OFF":
                    richTextBox1.Text = "\n" + "turned off";
                    spSynt.SpeakAsync("turned off");
                    char[] ch1 = { 'c' };
                    myPort.Write(ch1, 0, 1);

                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            spRec.RecognizeAsyncStop();
            btnDisable.Enabled = false;
            //myPort.Close();
        }
    }
}
