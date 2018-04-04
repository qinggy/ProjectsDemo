using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinoTechDataSyncService.Repository;
using DinoTechDataSyncService.Domain;
using DinoTechDataSyncService.Repository.Utilities;

namespace DinoTechDataSyncService.Business
{
    public class UserBusiness
    {
        public DinoTechDataSyncServiceHandler serviceHandler = new DinoTechDataSyncServiceHandler();

        public User ValidateUser(string userId, string password, string timeZoneOffSet)
        {
            try
            {
                User objUserModel = serviceHandler.ValidateUser(userId, password, timeZoneOffSet);
                return objUserModel;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public bool UpdateUserToken(int userId, string token)
        {

            try
            {
                Boolean response = serviceHandler.UpdateUserToken(userId, token);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool ValidateUserToken(string token)
        {
            try
            {
                Boolean response = serviceHandler.ValidateUserToken(token);
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
