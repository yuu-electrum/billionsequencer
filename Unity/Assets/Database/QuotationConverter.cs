using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    /// <summary>
    /// ��������N�I�[�e�[�V�����ň͂܂ꂽ���̂ɒu��������N���X
    /// </summary>
    public class QuotationConverter
    {
        public static string Convert(string value)
        {
            int parsedInt;
            float parsedFloat;
            double parsedDouble;

            bool isNumeric = int.TryParse(value, out parsedInt) || float.TryParse(value, out parsedFloat) || double.TryParse(value, out parsedDouble);
            if(!isNumeric && value == null)
            {
                value = "NULL";
            }
            else if(!isNumeric)
            {
                value = string.Format("'{0}'", value);
            }

            return value;
        }
    }
}