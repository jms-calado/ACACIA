using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class JsonRESTObjects
    {
        // We receive a JSON response, so define a class to deserialize the json into
        public class ResponseObject
        {
            [JsonProperty]
            public string Observation_ID { get; set; }
        }
        public class UserObject
        {
            public string Name { get; set; }
            public string Blog { get; set; }

            // This is deserialized using Json.NET, so use attributes as necessary
            [JsonProperty("created_at")]
            public DateTime CreatedAt { get; set; }
        }
        public class AffectObject
        {
            public string Bored { get; set; }
            public string Concentrated { get; set; }
            public string Confused { get; set; }
            public string Exited { get; set; }
            public string Frustrated { get; set; }
            public string Meditation { get; set; }
            public string Neutral_Affect { get; set; }
            public string Satisfaction { get; set; }
            public string Other_Affect { get; set; }
            public string Other_Affect_Name { get; set; }
            public string ObservationID { get; set; }
        }
        public class BehaviourObject
        {
            public string Active_Participation { get; set; }
            public string Attention { get; set; }
            public string Disengaged { get; set; }
            public string Engaged { get; set; }
            public string Inactive_Participation { get; set; }
            public string Off_Task { get; set; }
            public string On_Task { get; set; }
            public string Other_Behaviour { get; set; }
            public string Other_Behaviour_Name { get; set; }
            public string ObservationID { get; set; }
        }
        public class EmotionObject
        {
            public string Anger { get; set; }
            public string Contempt { get; set; }
            public string Disgust { get; set; }
            public string Fear { get; set; }
            public string Happiness { get; set; }
            public string Joy { get; set; }
            public string Neutral_Emotion { get; set; }
            public string Sadness { get; set; }
            public string Surprise { get; set; }
            public string ObservationID { get; set; }
        }
        public class ObservationObject
        {
            public string Duration { get; set; }
            public DateTime Date_Time { get; set; }
            public string Student { get; set; }
            public string Scenario { get; set; }
            public string Session { get; set; }
            public string Sensory_Component { get; set; }
        }
    }
}
