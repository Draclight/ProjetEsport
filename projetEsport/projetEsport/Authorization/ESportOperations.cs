using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetEsport.Authorization
{
    public class ESportOperations
    {
        public static OperationAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };
        public static OperationAuthorizationRequirement Read =
          new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };
        public static OperationAuthorizationRequirement Edit =
          new OperationAuthorizationRequirement { Name = Constants.EditOperationName };
        public static OperationAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };
    }

    public class Constants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string EditOperationName = "Edit";
        public static readonly string DeleteOperationName = "Delete";

        public static readonly string AdministrateursRole = "ADMINISTRATEUR";
        public static readonly string OrganisteursRole = "ORGANISATEUR";
        public static readonly string LicenciesRole = "LICENCIE";
        public static readonly string ConnexionRole = "CONNEXION";
    }
}
