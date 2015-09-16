﻿namespace Snippets4.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using NServiceBus;

    public class ScanningPublicApi
    {
        public void ScanningDefault()
        {
            #region ScanningDefault

            Configure.With();

            #endregion

        }

        public void ScanningExcludeByName()
        {
            #region ScanningExcludeByName

            IExcludesBuilder excludesBuilder = AllAssemblies
                .Except("MyAssembly1.dll")
                .And("MyAssembly2.dll");
            Configure.With(excludesBuilder);

            #endregion
        }

        public void ScanningListOfTypes()
        {
            IEnumerable<Type> myTypes = null;

            #region ScanningListOfTypes

            Configure.With(myTypes);

            #endregion

        }

        public void ScanningListOfAssemblies()
        {
            IEnumerable<Assembly> myListOfAssemblies = null;
            Assembly assembly2 = null;
            Assembly assembly1 = null;

            #region ScanningListOfAssemblies

            Configure.With(myListOfAssemblies);
            // or
            Configure.With(assembly1, assembly2);

            #endregion

        }

        public void ScanningIncludeByPattern()
        {
            #region ScanningIncludeByPattern

            IIncludesBuilder includesBuilder = AllAssemblies
                .Matching("NServiceBus")
                .And("MyCompany.")
                .And("SomethingElse");
            Configure.With(includesBuilder);

            #endregion
        }

        public void ScanningCustomDirectory()
        {
            #region ScanningCustomDirectory

            Configure.With(@"c:\my-custom-dir");

            #endregion

        }

        public void ScanningMixingIncludeAndExclude()
        {
            #region ScanningMixingIncludeAndExclude

            IExcludesBuilder excludesBuilder = AllAssemblies
                .Matching("NServiceBus")
                .And("MyCompany.")
                .Except("BadAssembly.dll");
            Configure.With(excludesBuilder);

            #endregion
        }

        public void ScanningUpgrade()
        {
            #region 5to6ScanningUpgrade

            IExcludesBuilder excludesBuilder = AllAssemblies
                .Matching("NServiceBus")
                .And("MyCompany.")
                .Except("BadAssembly1.dll")
                .And("BadAssembly2.dll");
            Configure.With(excludesBuilder);

            #endregion
        }

        #region ScanningConfigurationInNSBHost

        public class EndpointConfig : IConfigureThisEndpoint, IWantCustomInitialization
        {
            public void Init()
            {
                // use 'Configure' to configure scanning
            }
        }

        #endregion
    }
}