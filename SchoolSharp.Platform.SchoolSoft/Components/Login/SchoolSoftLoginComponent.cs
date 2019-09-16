﻿using SchoolSharp.Abstractions;
using SchoolSharp.Abstractions.Components.Login;
using SchoolSharp.Core.Components.Login;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSharp.Platform.SchoolSoft.Components.Login
{
    public class SchoolSoftLoginComponent : BaseLoginComponent<SchoolSoftSchoolPlatformClient>
    {
        public SchoolSoftLoginComponent(SchoolSoftSchoolPlatformClient client) : base(client)
        {

        }

        
        public override async Task<LoginResult> LoginAsync(UserCrededentials crededentials)
        {
            const string templateUrl = "https://sms1.schoolsoft.se/{0}/jsp/Login.jsp";
            string url = string.Format(templateUrl, crededentials.School);

            var result = await SchoolClient.Navigator.PostAsync(url, new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "action", "login" },
                { "usertype", crededentials.AccountType },
                { "ssusername", crededentials.Username },
                { "sspassword", crededentials.Password },
                { "button", "Logga in" }
            }));

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                return LoginResult.Build(false);

            return LoginResult.Build(true);
        }
    }
}