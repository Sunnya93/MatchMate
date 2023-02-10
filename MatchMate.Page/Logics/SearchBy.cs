using MatchMate.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MatchMate.Page.Logics
{
    public class SearchBy
    {
        public List<People> Consonent(List<People> peoples, string Value)
        {
            char[] chr = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
            string[] str = { "가", "까", "나", "다", "따", "라", "마", "바", "빠", "사", "싸", "아", "자", "짜", "차", "카", "타", "파", "하" };
            int[] chrint = { 44032, 44620, 45208, 45796, 46384, 46972, 47560, 48148, 48736, 49324, 49912, 50500, 51088, 51676, 52264, 52852, 53440, 54028, 54616, 55204 };

            string pattern = "";
            for (int i = 0; i < Value.Length; i++)
            {
                //초성만 입력되었을때
                if (Value[i] >= 'ㄱ' && Value[i] <= 'ㅎ')
                {
                    for (int j = 0; j < chr.Length; j++)
                    {
                        if (Value[i] == chr[j])
                        {
                            pattern += string.Format("[{0}-{1}]", str[j], (char)(chrint[j + 1] - 1));
                        }
                    }
                }
                //완성된 문자를 입력했을때 검색패턴 쓰기
                else if (Value[i] >= '가')
                {
                    //받침이 있는지 검사
                    int magic = ((Value[i] - '가') % 588);

                    //받침이 없을때.
                    if (magic == 0)
                    {
                        pattern += string.Format("[{0}-{1}]", Value[i], (char)(Value[i] + 27));
                    }

                    //받침이 있을때
                    else
                    {
                        magic = 27 - (magic % 28);
                        pattern += string.Format("[{0}-{1}]", Value[i], (char)(Value[i] + magic));
                    }
                }
                //영어를 입력했을때
                else if (Value[i] >= 'A' && Value[i] <= 'z')
                {
                    pattern += Value[i];
                }
                //숫자를 입력했을때.
                else if (Value[i] >= '0' && Value[i] <= '9')
                {
                    pattern += Value[i];
                }
            }

            return peoples!.Where(i => Regex.IsMatch(i.Name!, pattern)).ToList();
        }
    }
}
