using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SII
{
    public class FacturaElectronica
    {
        public void GenerateXMLDte()
        {
            DateTime dtNow = DateTime.Now;
            string dtNowStr = dtNow.ToString("yyyy-MM-ddThh:mm:ss");

            XmlDocument xml = new XmlDocument();
            xml.PreserveWhitespace = true;
            XmlNode nodeRoot = xml.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
            xml.AppendChild(nodeRoot);

            XmlNode nodeEnvioDTE = xml.CreateElement("EnvioDTE");
            CreateDTENode(xml, nodeEnvioDTE);

            XmlNode nodeSet = xml.CreateElement("SetDTE");
            XmlAttribute att1 = xml.CreateAttribute("ID");
            att1.Value = "SDTE323124722";
            nodeSet.Attributes.Append(att1);
            nodeEnvioDTE.AppendChild(nodeSet);

            XmlNode nodeCaratula = xml.CreateElement("Caratula");
            AddAttribute(xml, "version", "1.0", nodeCaratula);

            XmlNode nodeEmisor = xml.CreateElement("RutEmisor");
            nodeEmisor.InnerText = "76434673-4";
            XmlNode nodeEnvia = xml.CreateElement("RutEnvia");
            nodeEnvia.InnerText = "14271264-4";
            XmlNode nodeReceptor = xml.CreateElement("RutReceptor");
            nodeReceptor.InnerText = "70200800-K";
            XmlNode nodeSol = xml.CreateElement("FchResol");
            nodeSol.InnerText = "2014-10-21";
            XmlNode nodeNroResol = xml.CreateElement("NroResol");
            nodeNroResol.InnerText = "99";
            XmlNode nodeFrma = xml.CreateElement("TmstFirmaEnv");
            nodeFrma.InnerText = dtNowStr;

            XmlNode nodeSubTotDTE = xml.CreateElement("SubTotDTE");
            XmlNode nodeTpoDTE = xml.CreateElement("TpoDTE");
            nodeTpoDTE.InnerText = "34";
            XmlNode nodeNroDTE = xml.CreateElement("NroDTE");
            nodeNroDTE.InnerText = "1";
            nodeSubTotDTE.AppendChild(nodeTpoDTE);
            nodeSubTotDTE.AppendChild(nodeNroDTE);

            nodeCaratula.AppendChild(nodeEmisor);
            nodeCaratula.AppendChild(nodeEnvia);
            nodeCaratula.AppendChild(nodeReceptor);
            nodeCaratula.AppendChild(nodeSol);
            nodeCaratula.AppendChild(nodeNroResol);
            nodeCaratula.AppendChild(nodeFrma);
            nodeCaratula.AppendChild(nodeSubTotDTE);


            XmlNode nodeDTE = xml.CreateElement("DTE");
            AddAttribute(xml, "xmlns", "http://www.sii.cl/SiiDte", nodeDTE);
            AddAttribute(xml, "version", "1.0", nodeDTE);

            nodeSet.AppendChild(nodeCaratula);

            XmlNode nodeDocumento = xml.CreateElement("Documento");
            AddAttribute(xml, "ID", "MiPE76434673-6534", nodeDocumento);

            XmlNode nodeEncabezado = xml.CreateElement("Encabezado");
            XmlNode nodeIdDoc = xml.CreateElement("IdDoc");

            XmlNode nodeTipoDTE1 = xml.CreateElement("TipoDTE");
            nodeTipoDTE1.InnerText = "34";
            XmlNode nodeFolio = xml.CreateElement("Folio");
            nodeFolio.InnerText = "311";
            XmlNode nodeFchEmis = xml.CreateElement("FchEmis");
            nodeFchEmis.InnerText = dtNow.ToString("yyyy-MM-dd");

            nodeIdDoc.AppendChild(nodeTipoDTE1);
            nodeIdDoc.AppendChild(nodeFolio);
            nodeIdDoc.AppendChild(nodeFchEmis);

            XmlNode nodeEmisor1 = xml.CreateElement("Emisor");
            XmlNode nodeRUTEmisor = xml.CreateElement("RUTEmisor");
            nodeRUTEmisor.InnerText = "76434673-4";
            XmlNode nodeRznSoc = xml.CreateElement("RznSoc");
            nodeRznSoc.InnerText = "INSTITUTO DE CAPACITACION ADVANCE SPA";
            XmlNode nodeGiroEmis = xml.CreateElement("GiroEmis");
            nodeGiroEmis.InnerText = "INSTITUTO DE CAPACITACION";
            XmlNode nodeActeco = xml.CreateElement("Acteco");
            nodeActeco.InnerText = "809041";
            XmlNode nodeCdgSIISucur = xml.CreateElement("CdgSIISucur");
            nodeCdgSIISucur.InnerText = "079223009";
            XmlNode nodeDirOrigen = xml.CreateElement("DirOrigen");
            nodeDirOrigen.InnerText = "ALMIRANTE ZEGERS 764  12";
            XmlNode nodeCmnaOrigen = xml.CreateElement("CmnaOrigen");
            nodeCmnaOrigen.InnerText = "PROVIDENCIA";
            XmlNode nodeCiudadOrigen = xml.CreateElement("CiudadOrigen");
            nodeCiudadOrigen.InnerText = "SANTIAGO";

            nodeEmisor1.AppendChild(nodeRUTEmisor);
            nodeEmisor1.AppendChild(nodeRznSoc);
            nodeEmisor1.AppendChild(nodeGiroEmis);
            nodeEmisor1.AppendChild(nodeActeco);
            nodeEmisor1.AppendChild(nodeCdgSIISucur);
            nodeEmisor1.AppendChild(nodeDirOrigen);
            nodeEmisor1.AppendChild(nodeCmnaOrigen);
            nodeEmisor1.AppendChild(nodeCiudadOrigen);

            XmlNode nodeReceptor1 = xml.CreateElement("Receptor");
            XmlNode nodeRUTRecep = xml.CreateElement("RUTRecep");
            nodeRUTRecep.InnerText = "70200800-K";
            XmlNode nodeRznSocRecep = xml.CreateElement("RznSocRecep");
            nodeRznSocRecep.InnerText = "CORP DE CAPACITACION DE LA CONSTRUCCION";
            XmlNode nodeGiroRecep = xml.CreateElement("GiroRecep");
            nodeGiroRecep.InnerText = "OTROS SERVICIOS DESARROLLADOS POR PROFES";
            XmlNode nodeDirRecep = xml.CreateElement("DirRecep");
            nodeDirRecep.InnerText = "SANTA BEATRIZ 170  401";
            XmlNode nodeCmnaRecep = xml.CreateElement("CmnaRecep");
            nodeCmnaRecep.InnerText = "PROVIDENCIA";
            XmlNode nodeCiudadRecep = xml.CreateElement("CiudadRecep");
            nodeCiudadRecep.InnerText = "STGO";

            nodeReceptor1.AppendChild(nodeRUTRecep);
            nodeReceptor1.AppendChild(nodeRznSocRecep);
            nodeReceptor1.AppendChild(nodeGiroRecep);
            nodeReceptor1.AppendChild(nodeDirRecep);
            nodeReceptor1.AppendChild(nodeCmnaRecep);
            nodeReceptor1.AppendChild(nodeCiudadRecep);


            XmlNode nodeTotales = xml.CreateElement("Totales");
            XmlNode nodeMntExe = xml.CreateElement("MntExe");
            nodeMntExe.InnerText = "100000";
            XmlNode nodeMntTotal = xml.CreateElement("MntTotal");
            nodeMntTotal.InnerText = "100000";

            nodeTotales.AppendChild(nodeMntExe);
            nodeTotales.AppendChild(nodeMntTotal);

            nodeEncabezado.AppendChild(nodeIdDoc);
            nodeEncabezado.AppendChild(nodeEmisor1);
            nodeEncabezado.AppendChild(nodeReceptor1);
            nodeEncabezado.AppendChild(nodeTotales);

            XmlNode nodeDetalle = xml.CreateElement("Detalle");
            XmlNode nodeNroLinDet = xml.CreateElement("NroLinDet");
            nodeNroLinDet.InnerText = "1";
            XmlNode nodeNmbItem = xml.CreateElement("NmbItem");
            nodeNmbItem.InnerText = "E-Learning";
            XmlNode nodeDscItem = xml.CreateElement("DscItem");
            nodeDscItem.InnerText = "Nº de Curso: 755700-1; Registro Sence: 5360769; Nombre del Curso: Herramientas De Microsoft Excel Versión 16 Nivel Básico; Sence: 12.37.9529-85; Duración: 50 Hrs.; Inicio: 18-05-2017; Término: 15-06-2017; Lugar de Ejecución: 2 Norte 505 - Viña del Mar; Nº De Alumnos: 01; Razón Social Cliente: Empresa de Transacciones Max Facil S.A.; Rut Cliente: 76.094.784-9";
            XmlNode nodeQtyItem = xml.CreateElement("QtyItem");
            nodeQtyItem.InnerText = "1.00";
            XmlNode nodePrcItem = xml.CreateElement("PrcItem");
            nodePrcItem.InnerText = "100000.00";
            XmlNode nodeMontoItem = xml.CreateElement("MontoItem");
            nodeMontoItem.InnerText = "100000";

            nodeDetalle.AppendChild(nodeNroLinDet);
            nodeDetalle.AppendChild(nodeNmbItem);
            nodeDetalle.AppendChild(nodeDscItem);
            nodeDetalle.AppendChild(nodeQtyItem);
            nodeDetalle.AppendChild(nodePrcItem);
            nodeDetalle.AppendChild(nodeMontoItem);

            XmlNode nodeTED = xml.CreateElement("TED");
            AddAttribute(xml, "version", "1.0", nodeTED);
            XmlNode nodeDD = xml.CreateElement("DD");
            XmlNode nodeRE = xml.CreateElement("RE");
            nodeRE.InnerText = "76434673-4";
            XmlNode nodeTD = xml.CreateElement("TD");
            nodeTD.InnerText = "34";
            XmlNode nodeF = xml.CreateElement("F");
            nodeF.InnerText = "311";
            XmlNode nodeFE = xml.CreateElement("FE");
            nodeFE.InnerText = dtNow.ToString("yyyy-MM-dd");
            XmlNode nodeRR = xml.CreateElement("RR");
            nodeRR.InnerText = "70200800-K";
            XmlNode nodeRSR = xml.CreateElement("RSR");
            nodeRSR.InnerText = "CORP DE CAPACITACION DE LA CONSTRUCCION";
            XmlNode nodeMNT = xml.CreateElement("MNT");
            nodeMNT.InnerText = "100000";
            XmlNode nodeIT1 = xml.CreateElement("IT1");
            nodeIT1.InnerText = "E-Learning";

            nodeDD.AppendChild(nodeRE);
            nodeDD.AppendChild(nodeTD);
            nodeDD.AppendChild(nodeF);
            nodeDD.AppendChild(nodeFE);
            nodeDD.AppendChild(nodeRR);
            nodeDD.AppendChild(nodeRSR);
            nodeDD.AppendChild(nodeMNT);
            nodeDD.AppendChild(nodeIT1);

            XmlNode nodeCAF = xml.CreateElement("CAF");
            AddAttribute(xml, "version", "1.0", nodeCAF);

            XmlNode nodeDA = xml.CreateElement("DA");
            XmlNode nodeRE1 = xml.CreateElement("RE");
            nodeRE1.InnerText = "76434673-4";
            XmlNode nodeRS = xml.CreateElement("RS");
            nodeRS.InnerText = "INSTITUTO DE CAPACITACION ADVANCE SPA";
            XmlNode nodeTD11 = xml.CreateElement("TD");
            nodeTD11.InnerText = "34";
            XmlNode nodeRNG = xml.CreateElement("RNG");

            XmlNode nodeD = xml.CreateElement("D");
            nodeD.InnerText = "1";
            XmlNode nodeH = xml.CreateElement("H");
            nodeH.InnerText = "100";
            nodeRNG.AppendChild(nodeD);
            nodeRNG.AppendChild(nodeH);

            XmlNode nodeFA = xml.CreateElement("FA");
            nodeFA.InnerText = "2017-04-26";
            XmlNode nodeRSAPK = xml.CreateElement("RSAPK");

            XmlNode nodeM = xml.CreateElement("M");
            nodeM.InnerText = "2BTsovUEUrZGdCNO/z+JXOI1b6MGbd69kllxNkcX4zgpIP9pkepVMQWf5qDdlvC00fXUNYCuv3SClzS4hoOzww==";
            XmlNode nodeE = xml.CreateElement("E");
            nodeE.InnerText = "Aw==";
            nodeRSAPK.AppendChild(nodeM);
            nodeRSAPK.AppendChild(nodeE);

            XmlNode nodeIDK = xml.CreateElement("IDK");
            nodeIDK.InnerText = "100";

            nodeDA.AppendChild(nodeRE1);
            nodeDA.AppendChild(nodeRS);
            nodeDA.AppendChild(nodeTD11);
            nodeDA.AppendChild(nodeRNG);
            nodeDA.AppendChild(nodeFA);
            nodeDA.AppendChild(nodeRSAPK);
            nodeDA.AppendChild(nodeIDK);


            XmlNode nodeFRMA = xml.CreateElement("FRMA");
            AddAttribute(xml, "algoritmo", "SHA1withRSA", nodeFRMA);
            nodeFRMA.InnerText = "FxNig59oE9GrQDcOCNXFtb/KYZJpo+fmmyCVkxB6UZ3jcgLhKMJpaZdJ9HhzjQRUfmdATXT0NjBN5zJDvE+fFw==";
            nodeCAF.AppendChild(nodeDA);
            nodeCAF.AppendChild(nodeFRMA);

            XmlNode nodeTSTED = xml.CreateElement("TSTED");
            nodeTSTED.InnerText = dtNow.ToString("yyyy-MM-dd");

            nodeDD.AppendChild(nodeCAF);
            nodeDD.AppendChild(nodeTSTED);

            nodeTED.AppendChild(nodeDD);
            string xmlDD = nodeTED.InnerXml;

            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] bytesStrDD = ByteConverter.GetBytes(xmlDD);
            byte[] HashValue = new SHA1CryptoServiceProvider().ComputeHash(bytesStrDD);
            RSACryptoServiceProvider rsa = Utiles.CrearRsaDesdePEM(TimbreDD.PrivateKey);

            byte[] bytesSing = rsa.SignHash(HashValue, "SHA1");
            string FRMT1 = Convert.ToBase64String(bytesSing);

            XmlNode nodeFRMT = xml.CreateElement("FRMT");
            AddAttribute(xml, "algoritmo", "SHA1withRSA", nodeFRMT);
            nodeFRMT.InnerText = FRMT1;
            nodeTED.AppendChild(nodeFRMT);

            XmlNode nodeTmstFirma = xml.CreateElement("TmstFirma");
            nodeTmstFirma.InnerText = dtNowStr;
            nodeDocumento.AppendChild(nodeTmstFirma);

            nodeDocumento.AppendChild(nodeEncabezado);
            nodeDocumento.AppendChild(nodeDetalle);
            nodeDocumento.AppendChild(nodeTED);
            nodeDTE.AppendChild(nodeDocumento);
            nodeSet.AppendChild(nodeDTE);
            xml.AppendChild(nodeEnvioDTE);

            X509Certificate2 certificateTEst = ObtenerCertificado("");

           // X509Certificate2 certificate = GetCertificate2();
            //FirmarDocumentoXml(ref xml, certificateTEst, "#MiPE76434673-6534");
            FirmarDocumentoXml(ref xml, certificateTEst, "#MiPE76434673-6534");
            string salida = xml.OuterXml;
        }

        private static X509Certificate2 GetCertificate2()
        {
            X509Certificate2Collection certCollection = new X509Certificate2Collection();
            //X509KeyStorageFlags.
            byte[] rawData = ReadFile(@"C:\Users\skel3_000\Documents\Proyectos\Advance\SU3 julio 2013_Original\SU3\SII\Cert\Cert20170417.p7b");

            certCollection.Import(rawData, "fghjkl017", X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            //store.Open(OpenFlags.ReadWrite);

            //store.AddRange(certCollection);
            return certCollection[0];


            //X509Certificate2 x509 = new X509Certificate2();
            //byte[] rawData = ReadFile(@"C:\Users\skel3_000\Documents\Proyectos\Advance\SU3 julio 2013_Original\SU3\SII\Cert\Cert20170417.p7b");
            ////x509.Import(rawData);

            //var cert = new X509Certificate2(@"C:\Users\skel3_000\Documents\Proyectos\Advance\SU3 julio 2013_Original\SU3\SII\Cert\Cert20170417.p7b", 
            //                                                "xml38981923.,", 
            //                                                X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            //var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            //store.Open(OpenFlags.ReadWrite);
            //store.Add(cert);
            //store.Close();

            //return cert;
        }

        internal static byte[] ReadFile(string fileName)
        {
            FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            int size = (int)f.Length;
            byte[] data = new byte[size];
            size = f.Read(data, 0, size);
            f.Close();
            return data;
        }

        private static void AddAttribute(XmlDocument xml, string nameAtt, string valueAtt, XmlNode node)
        {
            XmlAttribute att1 = xml.CreateAttribute(nameAtt);
            att1.Value = valueAtt;
            node.Attributes.Append(att1);
        }

        private static void CreateDTENode(XmlDocument xml, XmlNode nodeEnvioDTE)
        {
            XmlAttribute att1 = xml.CreateAttribute("xmlns");
            att1.Value = "http://www.sii.cl/SiiDte";
            nodeEnvioDTE.Attributes.Append(att1);
            XmlAttribute att2 = xml.CreateAttribute("xmlns:xsi");
            att2.Value = "http://www.w3.org/2001/XMLSchema-instance";
            nodeEnvioDTE.Attributes.Append(att2);
            XmlAttribute att3 = xml.CreateAttribute("xsi:schemaLocation");
            att3.Value = "http://www.sii.cl/SiiDte EnvioDTE_v10.xsd";
            nodeEnvioDTE.Attributes.Append(att3);
        }

        public void GenerateF()
        {
            DateTime dtNow = DateTime.Now;
            string dtNowStr = dtNow.ToString("yyyy-MM-ddThh:mm:ss");
            DTEDefType dte = new DTEDefType();
            dte.version = 1.0m;

            DTEDefTypeDocumento document = new DTEDefTypeDocumento();

            document.Encabezado = new DTEDefTypeDocumentoEncabezado();
            document.Encabezado.IdDoc = new DTEDefTypeDocumentoEncabezadoIdDoc();
            document.Encabezado.Emisor = new DTEDefTypeDocumentoEncabezadoEmisor();
            document.Encabezado.Receptor = new DTEDefTypeDocumentoEncabezadoReceptor();
            document.Detalle = new List<DTEDefTypeDocumentoDetalle>();
            document.Encabezado.Totales = new DTEDefTypeDocumentoEncabezadoTotales();


            document.Encabezado.IdDoc.TipoDTE = DTEType.Item34;
            document.Encabezado.IdDoc.Folio = "1";
            document.Encabezado.IdDoc.FchEmis = dtNow;

            document.Encabezado.Emisor.RUTEmisor = "97975000-5";
            document.Encabezado.Emisor.RznSoc = "RUT DE PRUEBA";
            document.Encabezado.Emisor.GiroEmis = "Insumos de Computacion";
            //document.Encabezado.Emisor.Acteco = "";
            document.Encabezado.Emisor.CdgSIISucur = "1234";
            document.Encabezado.Emisor.DirOrigen = "Teatinos 120, Piso 4";
            document.Encabezado.Emisor.CmnaOrigen = "Santiago";
            document.Encabezado.Emisor.CiudadOrigen = "Santiago";


            document.Encabezado.Receptor.RUTRecep = "77777777-7";
            document.Encabezado.Receptor.RznSocRecep = "EMPRESA  LTDA";
            document.Encabezado.Receptor.GiroRecep = "COMPUTACION";
            document.Encabezado.Receptor.DirRecep = "SAN DIEGO 2222";
            document.Encabezado.Receptor.CmnaRecep = "PROVIDENCIA";
            document.Encabezado.Receptor.CiudadRecep = "SANTIAGO";


            document.Encabezado.Totales.MntNeto = "100000";
            document.Encabezado.Totales.TasaIVA = 19;
            document.Encabezado.Totales.IVA = "19000";
            document.Encabezado.Totales.MntTotal = "119000";

            DTEDefTypeDocumentoDetalle det1 = new DTEDefTypeDocumentoDetalle();
            det1.NroLinDet = "1";
            det1.CdgItem.Add(new DTEDefTypeDocumentoDetalleCdgItem { TpoCodigo = "INT1", VlrCodigo = "001" });
            det1.NmbItem = "Parlantes Multimedia 180W.";
            det1.QtyItem = 20;
            det1.PrcItem = 4500;
            det1.MontoItem = "90000";

            DTEDefTypeDocumentoDetalle det2 = new DTEDefTypeDocumentoDetalle();
            det2.NroLinDet = "2";
            det2.CdgItem.Add(new DTEDefTypeDocumentoDetalleCdgItem { TpoCodigo = "INT1", VlrCodigo = "0231" });
            det2.NmbItem = "Mouse Inalambrico PS/2";
            det2.QtyItem = 1;
            det2.PrcItem = 5000;
            det2.MontoItem = "5000";

            DTEDefTypeDocumentoDetalle det3 = new DTEDefTypeDocumentoDetalle();
            det3.NroLinDet = "3";
            det3.CdgItem.Add(new DTEDefTypeDocumentoDetalleCdgItem { TpoCodigo = "INT1", VlrCodigo = "1515" });
            det3.NmbItem = "Caja de Diskettes 10 Unidades";
            det3.QtyItem = 5;
            det3.PrcItem = 1000;
            det3.MontoItem = "5000";


            document.Detalle.Add(det1);
            document.Detalle.Add(det2);
            document.Detalle.Add(det3);

            document.TED = new DTEDefTypeDocumentoTED();
            document.TED.DD = new DTEDefTypeDocumentoTEDDD();
            document.TED.DD.RE = "97975000-5"; //RUT AD
            document.TED.DD.TD = DTEType.Item34;
            document.TED.DD.F = "60"; //consulta SII 
            document.TED.DD.FE = dtNow;
            document.TED.DD.RR = "77777777-7"; // RUT recibe fact
            document.TED.DD.RSR = "EMPRESA  LTDA"; //
            document.TED.DD.MNT = 119000;
            document.TED.DD.IT1 = "Parlantes Multimedia 180W.";


            document.TED.DD.CAF = new DTEDefTypeDocumentoTEDDDCAF();
            document.TED.DD.CAF.DA = new DTEDefTypeDocumentoTEDDDCAFDA();
            document.TED.DD.CAF.DA.RE = "76434673-4";
            document.TED.DD.CAF.DA.RS = "INSTITUTO DE CAPACITACION ADVANCE SPA";
            document.TED.DD.CAF.DA.TD = DTEType.Item34;
            document.TED.DD.CAF.DA.RNG = new DTEDefTypeDocumentoTEDDDCAFDARNG();
            document.TED.DD.CAF.DA.RNG.D = "1";
            document.TED.DD.CAF.DA.RNG.H = "100";
            document.TED.DD.CAF.DA.FA = new DateTime(2017, 4, 26);
            document.TED.DD.CAF.DA.IDK = 100;

            ASCIIEncoding ByteConverter = new ASCIIEncoding();



            DTEDefTypeDocumentoTEDDDCAFDARSAPK oPkDA = new DTEDefTypeDocumentoTEDDDCAFDARSAPK();
            oPkDA.M = Convert.FromBase64String("2BTsovUEUrZGdCNO/z+JXOI1b6MGbd69kllxNkcX4zgpIP9pkepVMQWf5qDdlvC00fXUNYCuv3SClzS4hoOzww==");
            oPkDA.E = Convert.FromBase64String("Aw==");
            document.TED.DD.CAF.DA.Item = oPkDA;

            document.TED.DD.CAF.FRMA = new DTEDefTypeDocumentoTEDDDCAFFRMA();
            document.TED.DD.CAF.FRMA.algoritmo = "SHA1withRSA";
            document.TED.DD.CAF.FRMA.Value = Convert.FromBase64String("FxNig59oE9GrQDcOCNXFtb/KYZJpo+fmmyCVkxB6UZ3jcgLhKMJpaZdJ9HhzjQRUfmdATXT0NjBN5zJDvE+fFw==");


            document.TED.DD.TSTED = dtNow;

            string DD = GetDD(dtNowStr);
            byte[] bytesStrDD = ByteConverter.GetBytes(DD);

            byte[] HashValue = new SHA1CryptoServiceProvider().ComputeHash(bytesStrDD);
            RSACryptoServiceProvider rsa = Utiles.CrearRsaDesdePEM(TimbreDD.PrivateKey);

            byte[] bytesSing = rsa.SignHash(HashValue, "SHA1");
            string FRMT1 = Convert.ToBase64String(bytesSing);


            document.TED.FRMT = new DTEDefTypeDocumentoTEDFRMT();
            document.TED.FRMT.Value = bytesSing;
            document.TmstFirma = dtNow;

            string endFinal = document.Serialize();
        }

        public string GetDD(string date)
        {
            StringBuilder sbDD = new StringBuilder();
            sbDD.Append("<DD>");
            sbDD.Append("<RE>97975000-5</RE>");
            sbDD.Append("<TD>33</TD>");
            sbDD.Append("<F>60</F>");
            sbDD.Append("<FE>2017-06-15</FE>");
            sbDD.Append("<RR>76434673-4</RR>");
            sbDD.Append("<RSR>EMPRESA  LTDA</RSR>");
            sbDD.Append("<MNT>119000</MNT>");
            sbDD.Append("<IT1>Parlantes Multimedia 180W.</IT1>");
            sbDD.Append("<CAF version=\"1.0\">");
            sbDD.Append("<DA>");
            sbDD.Append("<RE>76434673-4</RE>");
            sbDD.Append("<RS>INSTITUTO DE CAPACITACION ADVANCE SPA</RS>");
            sbDD.Append("<TD>34</TD>");
            sbDD.Append("<RNG>");
            sbDD.Append("<D>1</D>");
            sbDD.Append("<H>100</H>");
            sbDD.Append("</RNG>");
            sbDD.Append("<FA>2017-04-26</FA>");
            sbDD.Append("<RSAPK>");
            sbDD.Append("<M>2BTsovUEUrZGdCNO/z+JXOI1b6MGbd69kllxNkcX4zgpIP9pkepVMQWf5qDdlvC00fXUNYCuv3SClzS4hoOzww==</M>");
            sbDD.Append("<E>Aw==</E>");
            sbDD.Append("</RSAPK>");
            sbDD.Append("<IDK>100</IDK>");
            sbDD.Append("</DA>");
            sbDD.Append("<FRMA algoritmo=\"SHA1withRSA\">FxNig59oE9GrQDcOCNXFtb/KYZJpo+fmmyCVkxB6UZ3jcgLhKMJpaZdJ9HhzjQRUfmdATXT0NjBN5zJDvE+fFw==</FRMA>");
            sbDD.Append("</CAF>");
            sbDD.Append("<TSTED>" + date + "</TSTED>");
            sbDD.Append("</DD>");
            return sbDD.ToString();
        }

        public void TestEquals()
        {
            string DD = string.Empty;
            DD += "<DD><RE>97975000-5</RE><TD>33</TD><F>27</F><FE>2003-09-08</FE>";
            DD += "<RR>8414240-9</RR><RSR>JORGE GONZALEZ LTDA</RSR><MNT>502946</M";
            DD += "NT><IT1>Cajon AFECTO</IT1><CAF version=\"1.0\"><DA><RE>97975000-";
            DD += "5</RE><RS>RUT DE PRUEBA</RS><TD>33</TD><RNG><D>1</D><H>200</H>";
            DD += "</RNG><FA>2003-09-04</FA><RSAPK><M>0a4O6Kbx8Qj3K4iWSP4w7KneZYe";
            DD += "J+g/prihYtIEolKt3cykSxl1zO8vSXu397QhTmsX7SBEudTUx++2zDXBhZw==<";
            DD += "/M><E>Aw==</E></RSAPK><IDK>100</IDK></DA><FRMA algoritmo=\"SHA1";
            DD += "withRSA\">g1AQX0sy8NJugX52k2hTJEZAE9Cuul6pqYBdFxj1N17umW7zG/hAa";
            DD += "vCALKByHzdYAfZ3LhGTXCai5zNxOo4lDQ==</FRMA></CAF><TSTED>2003-09";
            DD += "-08T12:28:31</TSTED></DD>";

            ////
            //// Representa la clave privada rescatada desde el CAF que envía el SII
            //// para la prueba propuesta por ellos.
            ////
            string pk = string.Empty;
            pk += "MIIBOwIBAAJBANGuDuim8fEI9yuIlkj+MOyp3mWHifoP6a4oWLSBKJSrd3MpEsZd";
            pk += "czvL0l7t/e0IU5rF+0gRLnU1Mfvtsw1wYWcCAQMCQQCLyV9FxKFLW09yWw7bVCCd";
            pk += "xpRDr7FRX/EexZB4VhsNxm/vtJfDZyYle0Lfy42LlcsXxPm1w6Q6NnjuW+AeBy67";
            pk += "AiEA7iMi5q5xjswqq+49RP55o//jqdZL/pC9rdnUKxsNRMMCIQDhaHdIctErN2hC";
            pk += "IP9knS3+9zra4R+5jSXOvI+3xVhWjQIhAJ7CF0R0S7SIHHKe04NUURf/7RvkMqm1";
            pk += "08k74sdnXi3XAiEAlkWk2vc2HM+a1sCqQxNz/098ketqe7NuidMKeoOQObMCIQCk";
            pk += "FAMS9IcPcMjk7zI2r/4EEW63PSXyN7MFAX7TYe25mw==";


            //// 
            //// Este es el resultado que el SII indica debe obtenerse despues de crear
            //// el timbre sobre los datos expuestos.
            ////
            const string HTIMBRE = "pqjXHHQLJmyFPMRvxScN7tYHvIsty0pqL2LLYaG43jMmnfiZfllLA0wb32lP+HBJ/tf8nziSeorvjlx410ZImw==";


            //// //////////////////////////////////////////////////////////////////
            //// Generar timbre sobre los datos del tag DD utilizando la clave 
            //// privada suministrada por el SII en el archivo CAF
            //// //////////////////////////////////////////////////////////////////

            ////
            //// Calcule el hash de los datos a firmar DD
            //// transformando la cadena DD a arreglo de bytes, luego con
            //// el objeto 'SHA1CryptoServiceProvider' creamos el Hash del
            //// arreglo de bytes que representa los datos del DD
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] bytesStrDD = ByteConverter.GetBytes(DD);
            byte[] HashValue = new SHA1CryptoServiceProvider().ComputeHash(bytesStrDD);

            ////
            //// Cree el objeto Rsa para poder firmar el hashValue creado
            //// en el punto anterior. La clase FuncionesComunes.crearRsaDesdePEM()
            //// Transforma la llave rivada del CAF en formato PEM a el objeto
            //// Rsa necesario para la firma.
            RSACryptoServiceProvider rsa = Utiles.CrearRsaDesdePEM(pk);

            ////
            //// Firme el HashValue ( arreglo de bytes representativo de DD )
            //// utilizando el formato de firma SHA1, lo cual regresará un nuevo 
            //// arreglo de bytes.
            byte[] bytesSing = rsa.SignHash(HashValue, "SHA1");

            ////
            //// Recupere la representación en base 64 de la firma, es decir de
            //// el arreglo de bytes 
            string FRMT1 = Convert.ToBase64String(bytesSing);

            ////
            //// Comprobación del timbre generado por nuestra rutina contra el
            //// valor 
            if (HTIMBRE.Equals(FRMT1))
            {
                Console.WriteLine("Comprobacion OK");
            }
            else
            {
                Console.WriteLine("Comprobacion NOK");
            }
            Console.ReadLine();
        }

        public static void FirmarDocumentoXml(ref XmlDocument xmldocument, X509Certificate2 certificado, string referenciaUri)
        {
            ////
            //// Cree el objeto SignedXml donde xmldocument
            //// representa el documento DTE preparado para
            //// ser firmado. Recuerde que debe ser abierto 
            //// con la propiedad PreserveWhiteSpace = true
            SignedXml signedXml = new SignedXml(xmldocument);

            ////
            //// Agregue la clave privada al objeto signedXml
            signedXml.SigningKey = certificado.PrivateKey;

            ////
            //// Recupere el objeto signature desde signedXml
            Signature XMLSignature = signedXml.Signature;

            ////
            //// Cree la refrerencia al documento DTE
            //// recuerde que la referencia tiene el 
            //// formato '#reference'
            //// ejemplo '#DTE001'
            Reference reference = new Reference();
            reference.Uri = referenciaUri;

            ////
            //// Agregue la referencia al objeto signature
            XMLSignature.SignedInfo.AddReference(reference);
            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new RSAKeyValue((RSA)certificado.PrivateKey));

            ////
            //// Agregar información del certificado x509
            keyInfo.AddClause(new KeyInfoX509Data(certificado));
            XMLSignature.KeyInfo = keyInfo;

            ////
            //// Calcule la firma y recupere la representacion
            //// de la firma en un objeto xmlElement
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            ////
            //// Inserte la firma en el documento DTE
            xmldocument.DocumentElement.AppendChild(xmldocument.ImportNode(xmlDigitalSignature, true));

        }

        /// <summary>
        /// Recupera un determinado certificado para poder firmar un documento
        /// </summary>
        /// <param name="CN">Nombre del certificado que se busca
        /// <returns>X509Certificate2</returns>
        public static X509Certificate2 ObtenerCertificado(string CN)
        {
            X509Store objStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            X509Certificate2 certificado = null;
            objStore.Open(OpenFlags.ReadOnly);
            short i = 1;
            foreach (X509Certificate2 objCert in objStore.Certificates)
            {
                if (i != 1)
                {
                    if (objCert.SubjectName.Name.Contains("CN=Emerson Andres Gutiérrez Molina"))
                    {
                        certificado = objCert;
                        break;
                    }
                }
                i++;
            }
            objStore.Close();
            return certificado;    

            //if (string.IsNullOrEmpty(CN) || CN.Length == 0)
            //    return certificado;

            //try
            //{

            //    X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            //    store.Open(OpenFlags.ReadOnly);
            //    X509Certificate2Collection Certificados1 = (X509Certificate2Collection)store.Certificates;
            //    X509Certificate2Collection Certificados2 = Certificados1.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            //    X509Certificate2Collection Certificados3 = Certificados2.Find(X509FindType.FindBySubjectName, CN, false);
            //    //// Si hay certificado disponible envíe el primero
            //    if (Certificados3 != null && Certificados3.Count != 0)
            //        certificado = Certificados3[0];

            //    store.Close();
            //}
            //catch (Exception)
            //{
            //    certificado = null;
            //}
            //return certificado;

        }
    }
}
