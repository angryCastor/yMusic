using System;
using System.Net;

namespace YandexMusic{
    class Cookie{
        private static Cookie instance;
         private CookieContainer cookieContainer;
        private Cookie(){}

        public static Cookie GetInstance()
        {
            if (instance == null)
                instance = new Cookie();
            return instance;
        }

        public CookieContainer GetCookieContainer(){
            if(cookieContainer == null){
                cookieContainer = new CookieContainer();
            }

            return cookieContainer;
        }
    }  
}