using System;

namespace UsWebServices.Data
{
    public class ConstantsUrl
    {
        public const String REST_SERVICE_URL = "http://localhost:59648/api/";
        public const String ORDER = REST_SERVICE_URL + "order";
        public const String CUSTOMER = REST_SERVICE_URL + "customer";
        public const String SERVICETYPES = REST_SERVICE_URL + "servicetypes";
        public const String ERROR_MESSAGE = "Unable to connect to server. Please return later!";
    }
}
