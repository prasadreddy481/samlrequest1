namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.IdentityModel.Tokens;
    using ComponentSpace.SAML2.Profiles.SSOBrowser;
    using System.IO;
    using System.ServiceModel.Security;

    class Program
    {
        static void Main(string[] args)
        {
            var x = createSamlAssertion();

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Encoding = Encoding.UTF8
            };
            using (var stringWriter = new StringWriter(sb))
            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
            using (var dictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter))
            {
                var samlAssertSerializer = new SamlSerializer();
                var secTokenSerializer = new WSSecurityTokenSerializer();
                x.WriteXml(
                    dictionaryWriter,
                    samlAssertSerializer,
                    secTokenSerializer
                );
            }
        }

        private static SamlAssertion createSamlAssertion()
        {
            // Here we create some SAML assertion with ID and Issuer name. 
            SamlAssertion assertion = new SamlAssertion();
            assertion.AssertionId = "AssertionID";
            assertion.Issuer = "ISSUER";
            // Create some SAML subject. 
            SamlSubject samlSubject = new SamlSubject();
            samlSubject.Name = "My Subject";

            // 
            // Create one SAML attribute with few values. 
            SamlAttribute attr = new SamlAttribute();
            attr.Namespace = "http://daenet.eu/saml";
            attr.AttributeValues.Add("Some Value 1");
            //attr.AttributeValues.Add("Some Value 2");

            attr.Name = "My ATTR Value";

            SamlAttribute attr1 = new SamlAttribute();
            attr1.Namespace = "http://daenet.eu/saml";
            attr1.AttributeValues.Add("Some Value 2");
            //attr.AttributeValues.Add("Some Value 2");

            attr1.Name = "My ATTR Value 2";

            SamlAttribute attr2 = new SamlAttribute();
            attr2.Namespace = "http://daenet.eu/saml";
            attr2.AttributeValues.Add("Some Value 3");
            //attr.AttributeValues.Add("Some Value 2");

            attr2.Name = "My ATTR Value 3";

            // 
            // Now create the SAML statement containing one attribute and one subject. 
            SamlAttributeStatement samlAttributeStatement = new SamlAttributeStatement();
            samlAttributeStatement.Attributes.Add(attr);
            samlAttributeStatement.Attributes.Add(attr1);
            samlAttributeStatement.Attributes.Add(attr2);
            samlAttributeStatement.SamlSubject = samlSubject;

            // Append the statement to the SAML assertion. 
            assertion.Statements.Add(samlAttributeStatement);

            //return assertion
            return assertion;

        }
    }
}
