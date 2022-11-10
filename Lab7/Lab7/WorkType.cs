using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab7
{
    public class WorkType
    {
        public string Date { get; }
        public string Brend { get; }
        public string Price { get; }
        public string TypeWork { get; set; }

        public WorkType(string date, string brend, string price)
        {
            Date = date;
            Brend = brend;
            Price = price;
        }

        public string ShowInfo()
        {
            return $"Дата - {Date}; Бренд - {Brend}; Цена - {Price}; Тип работы - {TypeWork}\n";
        }

        public bool CheckDate()
        {
            bool testString = Regex.IsMatch(Regex.Replace(Date, " ", ""), @"\d\d.\d\d.\d\d") && Date.Length == 8;
            if (testString)
            {
                bool testMonth = (Convert.ToInt32(Date.Split('.')[1]) > 0 && Convert.ToInt32(Date.Split('.')[1]) <= 12);
                bool testYear = (Convert.ToInt32(Date.Split('.')[2]) >= 0 && Convert.ToInt32(Date.Split('.')[2]) <= 22);
                bool testDay = ((new int[7] { 1, 3, 5, 7, 8, 10, 12 }).Contains(Convert.ToInt32(Date.Split('.')[1])) && Convert.ToInt32(Date.Split('.')[0]) <= 31)
                || (Convert.ToInt32(Date.Split('.')[1]) == 2 && Convert.ToInt32(Date.Split('.')[0]) <= 28) ||
                ((new int[4] { 4, 6, 9, 11 }).Contains(Convert.ToInt32(Date.Split('.')[1])) && Convert.ToInt32(Date.Split('.')[0]) <= 30);
                return testString && testMonth && testYear && testDay;
            }
            return false;
        }

        public bool CheckDateBetween(string leftDate, string rightDate)
        {
            var Ldate = new DateTime(Convert.ToInt32($"20{leftDate.Split('.')[2]}"), Convert.ToInt32(leftDate.Split('.')[1]), Convert.ToInt32(leftDate.Split('.')[0]));
            var Rdate = new DateTime(Convert.ToInt32($"20{rightDate.Split('.')[2]}"), Convert.ToInt32(rightDate.Split('.')[1]), Convert.ToInt32(rightDate.Split('.')[0]));
            var Ndate = new DateTime(Convert.ToInt32($"20{Date.Split('.')[2]}"), Convert.ToInt32(Date.Split('.')[1]), Convert.ToInt32(Date.Split('.')[0]));
            return Ndate >= Ldate && Ndate <= Rdate;
        }

        public bool ChecBrend()
        {
            return Brend.All(char.IsLetter);
        }

        public bool CheckPrice()
        {
            return Price.All(char.IsDigit);
        }
    }

    public class Work1 : WorkType
    {
        public Work1(string date, string brend, string price) : base(date, brend, price)    
        {
            TypeWork = "Замена шин";   
        }
    }

    public class Work2 : WorkType
    {
        public Work2(string date, string brend, string price) : base(date, brend, price)
        {
            TypeWork = "Ремонт проколов";
        }
    }

    public class Work3 : WorkType
    {
        public Work3(string date, string brend, string price) : base(date, brend, price)
        {
            TypeWork = "Балансировка колес";
        }
    }

    public class Work4 : WorkType
    {
        public Work4(string date, string brend, string price) : base(date, brend, price)
        {
            TypeWork = "Развал-схождение";
        }
    }

    public class MainClass
    {
        private List<WorkType> Works = new List<WorkType>();

        public void AddNewWork(WorkType work)
        {
            Works.Add(work);
        }

        public void ShowAllData(System.Windows.Controls.Label label)
        {
            string resault = "";
            for(int i = 0; i < Works.Count; i++)
            {
                resault += Works[i].ShowInfo();
            }
            label.Content = resault;
        }

        public void GetAverageByBrend(System.Windows.Controls.Label label, string workType)
        {
            string resault = "";
            List<string> brend = new List<string> { };
            for (int i = 0; i < Works.Count; i++)
            {
                if (!brend.Contains(Works[i].Brend))
                {
                    brend.Add(Works[i].Brend);
                }
            }
            for (int i = 0; i < brend.Count; i++)
            {
                int kol = 0;
                int sum = 0;
                for(int j = 0; j < Works.Count; j++)
                {
                    if (brend[i] == Works[j].Brend)
                    {
                        if(Works[j].TypeWork == workType)
                        {
                            kol++;
                        }
                        sum++;
                    }
                }
                resault += $"{brend[i]}: из {sum} машин(ы) {kol} машина(ы) выполнила(и) ({workType})\n";
            }
            label.Content = resault;
        }

        public void GetTheMostTypeWorkByBrend(System.Windows.Controls.Label label, string Brend)
        {
            bool brendExist = false;
            for(int i = 0; i < Works.Count; i++)
            {
                if (Works[i].Brend == Brend)
                {
                    brendExist = true;
                    break;
                }
            }
            if (brendExist)
            {
                List<string> unicWorks = new List<string>(); 
                for(int i = 0; i < Works.Count; i++)
                {
                    if (!unicWorks.Contains(Works[i].TypeWork))
                    {
                        unicWorks.Add(Works[i].TypeWork);
                    }
                }
                int[] sumOfWorks = new int[unicWorks.Count];
                for(int i = 0; i < unicWorks.Count; i++)
                {
                    for(int j = 0; j < Works.Count; j++)
                    {
                        if (unicWorks[i] == Works[j].TypeWork && Works[j].Brend == Brend)
                        {
                            sumOfWorks[i] += 1;
                        }
                    }
                }

                for(int i = 0; i < sumOfWorks.Length; i++)
                {
                    for(int j = 0; j < sumOfWorks.Length; j++)
                    {
                        if (sumOfWorks[i] > sumOfWorks[j])
                        {
                            var tmp1 = sumOfWorks[i];
                            sumOfWorks[i] = sumOfWorks[j];
                            sumOfWorks[j] = tmp1;
                            var tmp2 = unicWorks[i];
                            unicWorks[i] = unicWorks[j];
                            unicWorks[j] = tmp2;
                        }
                    }
                }
                label.Content = $"Для {Brend} самая популярная работа {unicWorks[0]} ({sumOfWorks[0]} раз)";
            }
            else
            {
                label.Content = "Такого бренда нет";
            }
        }

        public void GetSumPriceByTypeWork(System.Windows.Controls.Label label, string dateL, string dateR)
        {
            string resault = $"За период {dateL} - {dateR}\n";
            List<string> unicWorks = new List<string>();
            for (int i = 0; i < Works.Count; i++)
            {
                if (!unicWorks.Contains(Works[i].TypeWork))
                {
                    unicWorks.Add(Works[i].TypeWork);
                }
            }
            for(int i = 0; i < unicWorks.Count; i++)
            {
                int sum = 0;
                for(int j = 0; j < Works.Count; j++)
                {
                    if (unicWorks[i] == Works[i].TypeWork)
                    {
                        sum += Convert.ToInt32(Works[i].Price);
                    }
                }
                resault += $"{unicWorks[i]} - {sum}\n";
            }
            label.Content = resault;
        }
    }
}