using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.T_Entities
{
    public class T_LocalStorage
    {
        //Multiple users could use same browser
        public List<UserStorageClass> Users { get; set; } = new List<UserStorageClass>();
        public class UserStorageClass
        {
            public string TypeDisposed { get; set; }
            public string IndividualIdentifierForDisposed { get; set; }
            public string UserName { get; set; } = "DefaultUser";  // Top-level user name
            public string TimeOfSave { get; set; } = "";  // Timestamp of save
            public UsersDataClass UsersData { get; set; } = new UsersDataClass();  // Contains the nested user data

            
            public UserStorageClass()
            {
                TimeOfSave = DateTime.Now.ToString("yyyyMMdd__HH_mm_ss_ffff");  // Default to UTC time on creation
                UsersData.Tiers = new List<UsersDataClass.TierData>
                {
                    new UsersDataClass.TierData
                    {
                        Level = "1",
                        Description = "Basic",
                        WhatAmI = "Example Complexity"
                    },
                      new UsersDataClass.TierData
                    {
                        Level = "1",
                        Description = "Basic",
                        WhatAmI = "Example Complexity"
                    },
                        new UsersDataClass.TierData
                    {
                        Level = "1",
                        Description = "Basic",
                        WhatAmI = "Example Complexity"
                    }

                };
            }
            //call default constructor plus this
            public UserStorageClass(string typeDisposed, string individualIdentifierForDisposed, string? userName = null, string? count = null, string? countType = null) : this()
            {
                UserName = userName ?? "DefaultUser";

                TypeDisposed = typeDisposed;
                IndividualIdentifierForDisposed = individualIdentifierForDisposed;
                // Pass the count and countType to the UsersData class constructor
                UsersData = new UsersDataClass(count, countType);
            }

      
            public class UsersDataClass
            {
                public UsersDataClass(string count = "0", string countType = "Default")
                {
                    Count = count;
                    CountType = countType;

                    Tiers = new List<UsersDataClass.TierData>
                    {
                        new UsersDataClass.TierData
                        {
                            Level = "1",
                            Description = "Basic",
                            WhatAmI = "Example Complexity"
                        },
                          new UsersDataClass.TierData
                        {
                            Level = "1",
                            Description = "Basic",
                            WhatAmI = "Example Complexity"
                        },
                            new UsersDataClass.TierData
                        {
                            Level = "1",
                            Description = "Basic",
                            WhatAmI = "Example Complexity"
                        }

                    };


                }
                public string Count { get; set; } = "0";
                public string CountType { get; set; } = "Default";
               
                public List<TierData> Tiers { get; set; } = new List<TierData>();  // Supports multiple tiers

                public class TierData
                {
                    public string WhatAmIFor { get; set; } = "JustToSimulateComplexity";
                    public string Level { get; set; } = "1";
                    public string Description { get; set; } = "Basic";
                    public string WhatAmI { get; set; } = "Just example of complexity";  
                }
            }
        }
    }
}
