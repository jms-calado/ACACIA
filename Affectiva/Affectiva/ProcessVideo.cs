using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Affectiva
{
    public partial class ProcessVideo: Affdex.ProcessStatusListener, Affdex.ImageListener
    {
        public ProcessVideo()
        {
            System.Console.WriteLine("Starting Interface...");
            detector = new Affdex.CameraDetector(0, 30, 30, 1, Affdex.FaceDetectorMode.LARGE_FACES);

            if (detector != null)
            {
                //detector.setClassifierPath("C/Program Files/Affectiva/AffdexSDK/data");
                detector.setClassifierPath("C:/Program Files/Affectiva/Affdex SDK/data");
                //C:\Program Files\Affectiva\Affdex SDK\data
                detector.setDetectAllEmotions(true);
                detector.setDetectAllExpressions(false);
                detector.setDetectAttention(true);
                detector.setDetectAllEmojis(false);
                detector.setDetectAllAppearances(false);
                System.Console.WriteLine("Face detector mode = " + detector.getFaceDetectorMode().ToString());
            }
            detector.setImageListener(this);
            detector.setProcessStatusListener(this);
                        
            Directory.CreateDirectory(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + "/Records/");
            Console.WriteLine("Ready.\r\nFile Creation Sample Rate: " + GlobalVars.SampleRate.ToString() + " miliseconds.");
        }
        public void onImageCapture(Affdex.Frame frame)
        {
            frame.Dispose();
        }
        public void onImageResults(Dictionary<int, Affdex.Face> faces, Affdex.Frame frame)
        {
            process_fps = 1.0f / (frame.getTimestamp() - process_last_timestamp);
            process_last_timestamp = frame.getTimestamp();
            System.Console.WriteLine(" pfps: {0}", process_fps.ToString());
            this.faces = faces;
            String Attention = null;
            String Anger = null;
            String Contempt = null;
            String Disgust = null;
            String Engagement = null;
            String Fear = null;
            String Joy = null;
            String Sadness = null;
            String Surprise = null;
            String Valence = null;

            foreach (KeyValuePair<int, Affdex.Face> pair in faces)
            {
                Affdex.Face face = pair.Value;

                foreach (PropertyInfo prop in typeof(Affdex.Expressions).GetProperties())
                {
                    if (prop.Name == "Attention")
                    {
                        float attention = (float)prop.GetValue(face.Expressions, null);
                        Attention = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", attention);
                        //Attention = attention.ToString("0.00", CultureInfo.InvariantCulture);
                    }
                }
                foreach (PropertyInfo prop in typeof(Affdex.Emotions).GetProperties())
                {
                    float value = (float)prop.GetValue(face.Emotions, null);
                    String Value = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", value);
                    if (prop.Name == "Anger") Anger = Value;
                    if (prop.Name == "Contempt") Contempt = Value;
                    if (prop.Name == "Disgust") Disgust = Value;
                    if (prop.Name == "Engagement") Engagement = Value;
                    if (prop.Name == "Fear") Fear = Value;
                    if (prop.Name == "Joy") Joy = Value;
                    if (prop.Name == "Sadness") Sadness = Value;
                    if (prop.Name == "Surprise") Surprise = Value;
                    //if (prop.Name == "Valence") Valence = Value;
                }
            }

            if (exit_state == false)
            {
                //DateTime dateVal = DateTime.ParseExact(DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"), "yyyy-dd-M--HH-mm-ss", CultureInfo.InvariantCulture);
                double Start_Time = ConvertToUnixTimestamp(DateTime.Now);

                file.WriteLine("  <emotion xmlns=\"http://www.w3.org/2009/10/emotionml\" category-set=\"http://www.w3.org/TR/emotion-voc/xml#fsre-categories\" " +
                                "start=\"" + Start_Time + "\" duration=\"30\">");
                file.WriteLine("      <category name=\"anger\" value=\"" + Anger + "\"/>");
                file.WriteLine("      <category name=\"contempt\" value=\"" + Contempt + "\"/>");
                file.WriteLine("      <category name=\"disgust\" value=\"" + Disgust + "\"/>");
                file.WriteLine("      <category name=\"fear\" value=\"" + Fear + "\"/>");
                file.WriteLine("      <category name=\"joy\" value=\"" + Joy + "\"/>");
                file.WriteLine("      <category name=\"sadness\" value=\"" + Sadness + "\"/>");
                file.WriteLine("      <category name=\"surprise\" value=\"" + Surprise + "\"/>");
                file.WriteLine("  </emotion>");

                file.WriteLine("  <BEHAVIOR start=\"" + Start_Time + "\" duration=\"30\">");
                file.WriteLine("      <category name=\"Engaged\" value=\"" + Engagement + "\"/>");
                file.WriteLine("  </BEHAVIOR>");
                
                if (stopwatch.ElapsedMilliseconds > GlobalVars.SampleRate)
                {
                    file.WriteLine("</emotionml>");
                    file.Dispose();//?
                    Console.WriteLine("New file created");
                    FilePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + "/Records/" + "Affectiva " + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".xml";
                    //FilePath = "C:/Users/Admin/Dropbox/ano3s1/TESE/TESTES/Programas 1.0/Records/" + "Affectiva " + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".xml";
                    file = new StreamWriter(FilePath, true);
                    file.AutoFlush = true;
                    file.WriteLine("<emotionml version=\"1.0\" xmlns=\"http://www.w3.org/2009/10/emotionml\">");
                    stopwatch.Restart();
                }
            }

            frame.Dispose();
        }
        public void Start()
        {
            if (exit_state == true)
            {
                exit_state = false;

                Console.WriteLine("Recording");

                file = new StreamWriter(FilePath, true);

                file.AutoFlush = true;
                file.WriteLine("<emotionml version=\"1.0\" xmlns=\"http://www.w3.org/2009/10/emotionml\">");
                stopwatch.Start();

                detector.start();
            }
        }
        public void Stop()
        {
            if (exit_state == false)
            {
                exit_state = true;

                detector.stop();
                
                Console.WriteLine("Recording Stopped");
                
                file.WriteLine("</emotionml>");
                //file.Close();
            }
        }
        public void onProcessingException(Affdex.AffdexException A_0)
        {
            System.Console.WriteLine("Encountered an exception while processing {0}", A_0.ToString());
        }
        public void onProcessingFinished()
        {
            System.Console.WriteLine("Processing finished successfully");
        }

        private float process_last_timestamp = -1.0f;
        private float process_fps = -1.0f;
        
        private Dictionary<int, Affdex.Face> faces { get; set; }
        private Affdex.Detector detector { get; set; }

        bool exit_state = true;
        Stopwatch stopwatch = new Stopwatch();
        
        StreamWriter file = null;
        //string FilePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + "/Records/" + "Affectiva " + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".xml";
        string FilePath = @"C:\Users\Admin\Dropbox\ano3s1\TESE\Aplicação GIT\ACACIA\Records" + "Affectiva " + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".xml";

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            //http://stackoverflow.com/questions/3354893/how-can-i-convert-a-datetime-to-the-number-of-seconds-since-1970
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalMilliseconds);
        }
        
    }
}
