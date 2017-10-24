using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static DataManager.JsonRESTObjects;

namespace DataManager
{
    // Define an interface representing the API
    // GitHub requires a User-Agent header, so specify one
    [Header("User-Agent", "DataManager")]
    [AllowAnyStatusCode]
    [SerializationMethods(Body = BodySerializationMethod.Serialized)]
    public interface IAcaciaApi
    {
        /*
        // The [Get] attribute marks this method as a GET request
        // The "list" is a relative path the a base URL
        // "{class_type}" is a placeholder in the URL: the value from the "class_type" method parameter is used
        [Get("list/{class_type}")]
        Task<UserObject> GetClassIndividuals([Path] string class_type);

        [Get("list/{individual_properties}")]
        Task<UserObject> GetIndividualProperties([Path] string individual_properties);
        */

        [Post("insert/observation/Digital")]
        Task<Response<ResponseObject>> PostObservation([Body] ObservationObject observationObject);

        [Post("insert/affect")]
        Task<HttpResponseMessage> PostAffect([Body] AffectObject affectObject);

        [Post("insert/behaviour")]
        Task<HttpResponseMessage> PostBehaviour([Body] BehaviourObject behaviourObject);

        [Post("insert/emotion")]
        Task<HttpResponseMessage> PostEmotion([Body] EmotionObject emotionObject);
    }
}
