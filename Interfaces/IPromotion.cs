using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Interfaces
{
    public interface IPromotion : IDisposable
    {
        List<Models.Promotion> GetPromotions();
        List<Models.Promotion> GetPromotionsById(int id);
        int InsertPromotion(string code,
                          string title,
                          string description,
                          DateTime expires_date,
                          string theme,
              int discount
                          );

        int removePromotion(int id_promotion);
        int UpdatePromotion(int id_promotion, string code,
                          string title,
                          string description,
                          DateTime expires_date,
                          string theme,
              int discount);
    }
}
