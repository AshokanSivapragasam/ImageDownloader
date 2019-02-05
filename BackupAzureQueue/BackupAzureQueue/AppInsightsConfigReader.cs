using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BackupAzureQueue
{
    /// <summary>
    /// ConfigurationSections enumeration used to access ApplicationInsights.config file.
    /// </summary>
    [Flags]
    public enum ConfigurationSections
    {
        /// <summary>
        /// Accessing None.
        /// </summary>
        None = 0,
        /// <summary>
        /// Accessing ServerAnalytics section.
        /// </summary>
        ServerAnalytics = 1,
        /// <summary>
        /// Accessing MMAOutputChannels section.
        /// </summary>
        MMAOutputChannels = 2,
        /// <summary>
        /// Accessing APMSettings section.
        /// </summary>
        APMSettings = 4,
        /// <summary>
        /// Accessing MemoryEventSettings section.
        /// </summary>
        MemoryEventSettings = 8,
        /// <summary>
        /// Accessing PerformanceCounters section.
        /// </summary>
        PerformanceCounters = 16,
        /// <summary>
        /// Accessing ClientAnalytics section.
        /// </summary>
        ClientAnalytics = 32,
        /// <summary>
        /// Accessing MonitoringAgent section.
        /// </summary>
        MonitoringAgent = 30
    }

    /// <summary>
    /// This class reads the ApplicationInsights.config file and provides read access to
    /// the configuration for the active profile.
    /// </summary>
    public class AppInsightsConfigReader
    {
        private static readonly XNamespace AINamespace = "http://schemas.microsoft.com/ApplicationInsights/2013/Settings";

        internal XDocument Configuration
        {
            get;
            private set;
        }

        /// <summary>
        /// Loads the specified configuration file.
        /// </summary>
        /// <param name="configFile">The full path to the ApplicationInsights.config file.</param>
        public static AppInsightsConfigReader LoadConfigurationFile(string configFile)
        {
            if (File.Exists(configFile))
            {
                return new AppInsightsConfigReader
                {
                    Configuration = XDocument.Load(configFile)
                };
            }
            throw new FileNotFoundException("Unable to load the specified configuration file", configFile);
        }

        /// <summary>
        /// Gets the configuration for the corresponding configuration section.
        /// </summary>
        public string GetConfiguration(ConfigurationSections configSection)
        {
            XDocument xDocument = null;
            if (this.Configuration != null)
            {
                XElement root = this.Configuration.Root;
                XElement xElement = root.Element(AppInsightsConfigReader.AINamespace + "ActiveProfile");
                XElement xElement2 = root.Element(AppInsightsConfigReader.AINamespace + "Profiles");
                string activeProfile = (xElement != null) ? xElement.Value : null;
                if (xElement2 != null && activeProfile != null)
                {
                    XElement xElement3 = xElement2.Element(AppInsightsConfigReader.AINamespace + "Defaults");
                    xElement = (
                        from e in xElement2.Elements(AppInsightsConfigReader.AINamespace + "Profile")
                        where e.Attribute("name") != null && string.Equals(e.Attribute("name").Value, activeProfile)
                        select e).FirstOrDefault<XElement>();
                    if (xElement != null)
                    {
                        xDocument = new XDocument(new object[]
						{
							new XElement(AppInsightsConfigReader.AINamespace + "ProfileSettings", (xElement3 != null) ? xElement3.Elements() : null)
						});
                        xDocument = AppInsightsConfigReader.MergeActiveProfile(xDocument, xElement);
                        if (!AppInsightsConfigReader.IsFlagSet(configSection, ConfigurationSections.ServerAnalytics))
                        {
                            XElement xElement4 = xDocument.Descendants(AppInsightsConfigReader.AINamespace + "ServerAnalytics").FirstOrDefault<XElement>();
                            if (xElement4 != null)
                            {
                                xElement4.Remove();
                            }
                        }
                        if (!AppInsightsConfigReader.IsFlagSet(configSection, ConfigurationSections.ClientAnalytics))
                        {
                            XElement xElement5 = xDocument.Descendants(AppInsightsConfigReader.AINamespace + "ClientAnalytics").FirstOrDefault<XElement>();
                            if (xElement5 != null)
                            {
                                xElement5.Remove();
                            }
                        }
                        if (!AppInsightsConfigReader.IsFlagSet(configSection, ConfigurationSections.MMAOutputChannels))
                        {
                            XElement xElement6 = xDocument.Descendants(AppInsightsConfigReader.AINamespace + "MMAOutputChannels").FirstOrDefault<XElement>();
                            if (xElement6 != null)
                            {
                                xElement6.Remove();
                            }
                        }
                        if (!AppInsightsConfigReader.IsFlagSet(configSection, ConfigurationSections.APMSettings))
                        {
                            XElement xElement7 = xDocument.Descendants(AppInsightsConfigReader.AINamespace + "APMSettings").FirstOrDefault<XElement>();
                            if (xElement7 != null)
                            {
                                xElement7.Remove();
                            }
                        }
                        if (!AppInsightsConfigReader.IsFlagSet(configSection, ConfigurationSections.MemoryEventSettings))
                        {
                            XElement xElement8 = xDocument.Descendants(AppInsightsConfigReader.AINamespace + "MemoryEventSettings").FirstOrDefault<XElement>();
                            if (xElement8 != null)
                            {
                                xElement8.Remove();
                            }
                        }
                        if (!AppInsightsConfigReader.IsFlagSet(configSection, ConfigurationSections.PerformanceCounters))
                        {
                            XElement xElement9 = xDocument.Descendants(AppInsightsConfigReader.AINamespace + "PerformanceCounters").FirstOrDefault<XElement>();
                            if (xElement9 != null)
                            {
                                xElement9.Remove();
                            }
                        }
                    }
                }
            }
            if (xDocument == null)
            {
                return null;
            }
            return xDocument.ToString();
        }

        /// <summary>
        /// Get the names of the configuration profiles.
        /// </summary>
        /// <returns>A collection of configuration names.</returns>
        public IEnumerable<string> GetProfileNames()
        {
            return
                from p in this.Configuration.Descendants(AppInsightsConfigReader.AINamespace + "Profile")
                select p.Attribute("name").Value;
        }

        /// <summary>
        /// Get the value specified in the ActiveProfile element.
        /// </summary>
        /// <returns>The name of the active profile.</returns>
        public string GetActiveProfileValue()
        {
            return this.Configuration.Descendants(AppInsightsConfigReader.AINamespace + "ActiveProfile").Single<XElement>().Value;
        }

        /// <summary>
        /// Gets the value specified in the DebugBuildProfile element.
        /// </summary>
        /// <returns>The name of the profile to be used for debug builds.</returns>
        public string GetDebugBuildProfileValue()
        {
            return this.Configuration.Descendants(AppInsightsConfigReader.AINamespace + "DebugBuildProfile").Single<XElement>().Value;
        }

        private static XDocument MergeActiveProfile(XDocument defaultDocument, XElement activeProfileElement)
        {
            XElement root = defaultDocument.Root;
            foreach (XElement current in activeProfileElement.Elements())
            {
                XElement xElement = root.Descendants(current.Name).FirstOrDefault<XElement>();
                if (xElement == null)
                {
                    root.Add(current);
                }
                else
                {
                    xElement.Remove();
                    root.Add(AppInsightsConfigReader.MergeElements(xElement, current));
                }
            }
            return defaultDocument;
        }

        private static XElement MergeElements(XElement existingElement, XElement activeElement)
        {
            XElement xElement = new XElement(activeElement);
            foreach (XElement current in existingElement.Elements())
            {
                XElement xElement2 = xElement.Descendants(current.Name).FirstOrDefault<XElement>();
                if (xElement2 == null)
                {
                    xElement.Add(current);
                }
                else
                {
                    if (current.HasElements)
                    {
                        xElement2.Remove();
                        xElement.Add(AppInsightsConfigReader.MergeElements(current, xElement2));
                    }
                }
            }
            return xElement;
        }

        private static bool IsFlagSet(ConfigurationSections flags, ConfigurationSections flag)
        {
            return (flags & flag) == flag;
        }
    }
}
