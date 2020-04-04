using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiMoney.DataBase;

namespace ZhiMoney.Data
{
    public interface IInputDataModel
    {
        void AddRecord(string name, float summa);

        List<float> GetSumma();

        List<DateTime> GetDateTimes();

        float GetCurrentSumma();

        void AlgorthmSort(out DateTime[] date, out float[] summa, int days);

        IInputData GetLastElement();
    }
}
