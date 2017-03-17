using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace text_work
{
    public class text
    {
        public string changes(string cur, string copy,ref int poscur) //finding what was changed and marking it!
        {
            string cur2 = "";
            int change = 0;
            while (change < copy.Length && change < cur.Length && cur[change] == copy[change]) { change++; }
            for (int i = 0; i < change; i++) { cur2 += cur[i]; }
            cur2 += ";;;-3";
            if (copy.Length < cur.Length)
            {
                int dif = change;
                for (int i = change; i < dif + cur.Length - copy.Length; i++) { cur2 += cur[i]; change++; } change--;
                while (copy.Length > change && cur[change + cur.Length - copy.Length] != copy[change]) { cur2 += cur[change + cur.Length - copy.Length]; change++; }
                change += cur.Length - copy.Length;
                if (copy.Length > change)
                {
                    cur2 += ";;;-4";
                    poscur = change;
                    for (int i = change; i < cur.Length; i++) { cur2 += cur[i]; }
                }
                else
                {
                    cur2 += ";;;-4";
                }
            }
            else
            {
                if (copy.Length == cur.Length)
                {
                    while (copy.Length > change && cur[change] != copy[change]) { cur2 += cur[change]; change++; }
                    cur2 += ";;;-4";
                    poscur = change;
                    for (int i = change; i < cur.Length; i++) { cur2 += cur[i]; }
                }
                else
                {
                    cur2 += ";;;-4";
                    poscur = change;
                    for (int i = change; i < cur.Length; i++) { cur2 += cur[i]; }
                }
            }
            return cur2;
        }
        public string adding_to_other_changings(string cur,string prev)
        {
            int beg = cur.IndexOf(";;;-3");
            int len=cur.IndexOf(";;;-4")-beg;
            int b = prev.IndexOf(";;;-3");
            if (b == -1) { return cur; }
            int e = 0;
            string temp = "";
            string res = "";
            string prevcopy=prev;
            string curcopy=cur;
            if (beg==0)
            {
                if (b == 0)
                {
                    if (prev.Substring(5, prev.IndexOf(";;;-4") - 5) == cur.Substring(len + beg + 5, prev.Substring(5, prev.IndexOf(";;;-4") - 5).Length))
                    { return res = cur.Substring(0, len) + prev.Substring(5); }
                    else
                    {
                            e = prev.IndexOf(";;;-4");
                            temp = prev.Substring(5, e - 5);
                                int cb = cur.IndexOf(";;;-3");
                                int ce = cur.IndexOf(";;;-4");
                                res += ";;;-3" + cur.Substring(0, cb) + cur.Substring(cb + 5, ce - cb - 5);
                                ce += 5;
                                int i = 0;
                                while (i < temp.Length && cur[ce] != temp[i]) i++;
                                if (i < temp.Length)
                                { return res += temp.Substring(i, temp.Length - i) + ";;;-4" + prev.Substring(e + 5); }
                                else { return res += ";;;-4" + prev.Substring(e + 5); }
                    }
                }
                else
                {
                    temp = prev.Substring(b + 5, prev.IndexOf(";;;-4") - b - 5);
                    if (temp != cur.Substring(cur.IndexOf(";;;-4")+5, temp.Length))
                    { return res = cur.Substring(0, len + 5) + prev; }
                    else
                    {
                        int ce = cur.IndexOf(";;;-4");
                        res += cur.Substring(0, ce);
                        ce += 5;
                        int i = 0;
                        while (i < temp.Length && cur[ce] != temp[i]) i++;
                        if (i < temp.Length)
                        { return res += temp.Substring(i, temp.Length - i) + prev.Substring(prev.IndexOf(";;;-4")); }
                        else { return res += prev.Substring(b + 5); }

                    }
                }

            }
            else
            {
         
                    while(b>-1)
                    {
                        if (b==0)
                        {
                            e = prev.IndexOf(";;;-4");
                            temp = prev.Substring(5, e - 5);
                            if(temp==cur.Substring(0,temp.Length))
                            {
                                res += ";;;-3" + temp + ";;;-4";
                                prev = prev.Substring(e + 5);
                                cur = cur.Substring(temp.Length);
                                b = prev.IndexOf(";;;-3");
                            }
                            else
                            {
                                int cb = cur.IndexOf(";;;-3");
                                int ce = cur.IndexOf(";;;-4");
                                res += ";;;-3" + cur.Substring(0, cb) + cur.Substring(cb + 5, ce - cb -5);
                                ce += 5;
                                int i=0;
                                if (ce < cur.Length)
                                { while (i < temp.Length && cur[ce] != temp[i]) i++; }
                                if (i < temp.Length &&cur.Length>ce)
                                {return res += temp.Substring(i, temp.Length - i) + ";;;-4" + prev.Substring(e + 5); }
                                else { return res += ";;;-4" + prev.Substring(e + 5); }
                            }
                        }
                        else
                        {
                            temp = prev.Substring(0, b);
                            if(temp==cur.Substring(0,temp.Length))
                            {
                                res +=temp;
                                prev = prev.Substring(b);
                                cur = cur.Substring(temp.Length);
                                b = prev.IndexOf(";;;-3");
                            }
                            else
                            {
                                int ce = cur.IndexOf(";;;-4");
                                if(res!="")
                                {
                                    int cb = cur.IndexOf(";;;-3");
                                    ce = cur.IndexOf(";;;-4");
                                    if (cb==0)
                                    {
                                        res=res.Substring(0,res.Length-5);
                                        res += cur.Substring(cb + 5, ce - cb - 5);
                                    }
                                    else
                                    {
                                        res += cur.Substring(cb, ce - cb);
                                    }
                                }
                                else
                                {
                                    res += cur.Substring(0, ce);
                                }
                                ce += 5;
                                int i = 0;
                                while (i < temp.Length && cur[ce] != temp[i]) i++;
                                if (i < temp.Length)
                                {return res += ";;;-4" + temp.Substring(i, temp.Length - i) + prev.Substring(b); }
                                else { return res += prev.Substring(b + 5); }
                            }

                        }
                    }
            }
            if (curcopy.Substring(curcopy.Length - 5) == ";;;-4")
            {
                if (prevcopy.Substring(prevcopy.Length - 5) == ";;;-4")
                {
                    { return res = prevcopy.Substring(0, prevcopy.Length - 5) + curcopy.Substring(beg + 5); }
                }
                else
                {
                    return res = prevcopy + curcopy.Substring(beg);
                }
            }
            return res;
        }
    }
}
