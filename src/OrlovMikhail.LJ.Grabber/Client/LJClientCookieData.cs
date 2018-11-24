using System.Collections.Generic;
using System.Linq;

namespace OrlovMikhail.LJ.Grabber.Client
{
    public class LJClientCookieData : ILJClientData
    {
        private readonly Dictionary<string, string> _cookies;

        private LJClientCookieData()
        {
            _cookies = new Dictionary<string, string>();
        }

        public static LJClientCookieData FromString(string s)
        {
            // _ga=GA1.2.526309289.1415025641; ljuniq=4evFhaVhvkzzzzzzzzzzzzzzz5641%3Apgstats0; xtvrn=$528851$; xtan=-; 
            // xtant=1; ljident=2869171628.20480.0000; _ym_visorc_27737346=b; welcome_ljvideo=1; 
            // ljloggedin=v2:u1546277:s354:t1436793777:gb0fdeff6zzzzzzzzzzzzzzzzzz6bae87ddbf0627ec; 
            // BMLschemepref=dystopia; langpref=en_GB/1436793777; ljsession=v1:u1546277:s354:t1436792400:g95c3zzzzzzzzzzzzz13820f24b7699f357//1; 
            // ljdomsess.galkovsky=v1:u1546277:s354:t1436792400:gzzzzzzzzzzzz6b7bff785d2f5dfe4617dfae//1

            string cookieStringTrimmed = s.Trim()
                .Trim('"')
                .Trim();
            IEnumerable<string[]> cookiePairs = cookieStringTrimmed.Split(';')
                .Select(
                    z => z.Trim()
                        .Split('=')
                );

            LJClientCookieData ret = new LJClientCookieData();
            foreach (string[] pair in cookiePairs)
            {
                string key = pair[0];
                string value = pair[1];

                ret._cookies[key] = value;
            }

            return ret;
        }

        public Dictionary<string, string> GetCookiesToUse()
        {
            string[] values = {"ljloggedin", "ljsession", "ljdomsess"};

            var ret = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> kvp in _cookies)
            {
                if (values.Any(z => kvp.Key.Contains(z)))
                {
                    ret.Add(kvp.Key, kvp.Value);
                }
            }

            return ret;
        }

        public override string ToString()
        {
            return string.Join(
                "; "
                , GetCookiesToUse()
                    .Select(kvp => kvp.Key + "=" + kvp.Value)
            );
        }
    }
}