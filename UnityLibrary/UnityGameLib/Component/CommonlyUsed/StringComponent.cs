using System.Text;

namespace CommonlyUsed
{
    public static class StringComponent
    {
        /// <summary>
        /// 文字列を連結させる関数(最大5つまで)
        /// </summary>
        public static string AddString(string str0, string str1, string str2 = "", string str3 = "", string str4 = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(str0);
            sb.Append(str1);
            sb.Append(str2);
            sb.Append(str3);
            sb.Append(str4);
            string s = sb.ToString();
            return s;
        }

        /// <summary>
        /// 複数の文字列を連結させる関数
        /// 引数(string[]文字列の配列)
        /// </summary>
        public static string AddString(string[] str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string word in str)
            {
                sb.Append(word);
            }
            string text = sb.ToString();
            return text;
        }
    }
}
