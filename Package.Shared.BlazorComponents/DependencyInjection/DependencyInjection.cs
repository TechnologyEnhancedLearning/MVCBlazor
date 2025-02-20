using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Package.Shared.BlazorComponents.BaseComponents.Buttons;
using Package.Shared.BlazorComponents.BaseComponents.EditForm;
using Package.Shared.BlazorComponents.BaseComponents.Validation;
using Package.Shared.BlazorComponents.Components.Buttons;
using Package.Shared.BlazorComponents.Components.Lists;
using Package.Shared.Entities.Interfaces.ComponentInterfaces;
using Package.Shared.Services.Configuration.CharactersConfiguration;
using Package.Shared.Services.Configuration.CounterConfiguration;
using Package.Shared.Services.StateServices.CharacterStateServices;
using Package.Shared.Services.StateServices.T_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.DependencyInjection
{
    public static class DependencyInjection
    {
        //Not injecting logging because logging may already be in serverside or clientside and may not need to be changed. So just doing it in dot notation in program.cs is fine.
    }
}
