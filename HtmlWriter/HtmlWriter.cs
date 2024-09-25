using S = SourceData.ObjectStructure;
using T = TargetData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using SourceData.ObjectStructure;

namespace HtmlOutput
{
    public class HtmlWriter
    {
        public void Execute(IEnumerable<T.IAssetObject> topLevelAssets, string filePath)
        {
            TextWriter writer = new StreamWriter(filePath, false, Encoding.UTF8);

            // TextWriter writer = Console.Out;

            writeTop(writer);

            writeTree(writer, topLevelAssets);

            writeBottom(writer);

            writer.Close();
        }

        private void writeTop(TextWriter writer)
        {
            writeLine(writer, "<!DOCTYPE html>");
            writeLine(writer, "<html>");
            writeLine(writer, "<head>");
            writeLine(writer, "    <title>AEVK annleggsstruktur, migrering</title>");
            writeLine(writer, "    <link href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css\" rel=\"stylesheet\" type=\"text/css\" />");
            writeLine(writer, "    <link href=\"https://cdnjs.cloudflare.com/ajax/libs/bootstrap-treeview/1.2.0/bootstrap-treeview.min.css\" rel=\"stylesheet\" type=\"text/css\" />");
            writeLine(writer, "</head>");
            writeLine(writer, "<body>");
            writeLine(writer, "\t<div class=\"container\">");
            writeLine(writer, "\t\t<div class=\"row\">");
            writeLine(writer, "\t\t\t<div class=\"col-sm-5\" style=\"height:800px;overflow-y: scroll;\">");
            writeLine(writer, "\t\t\t\t<div id=\"myTree\"></div>");
            writeLine(writer, "\t\t\t</div>");
            writeLine(writer, "\t\t\t<div class=\"col-sm-7\">");
            writeLine(writer, "\t\t\t\t<div class=\"panel-group\">");
            writeLine(writer, "\t\t\t\t\t<div class=\"panel panel-info\">");
            writeLine(writer, "\t\t\t\t\t\t<div class=\"panel-heading\" id=\"detail-panel-source-heading\">IFS</div>");
            writeLine(writer, "\t\t\t\t\t\t<div class=\"panel-body\" id=\"detail-panel-ifs\">");

            // N
            writeLine(writer, "\t<div class=\"container\">");
            writeLine(writer, "\t\t<div class=\"row\">");
            writeLine(writer, "\t\t\t<div class=\"col-sm-3\">");



            writeLine(writer, "\t\t\t\t\t\t\t<form>");
            writeLine(writer, "\t\t\t\t\t\t\t\t<div class=\"form-group\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-source-ebl\">EBL id:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-source-ebl\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-source-description\">Beskrivelse:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-source-description\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-source-type\">Type:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-source-type\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t</div>");
            writeLine(writer, "\t\t\t\t\t\t\t</form>");

            // N
            writeLine(writer, "\t\t\t</div>");
            writeLine(writer, "\t\t\t<div class=\"col-sm-3\">");


            writeLine(writer, "\t\t\t\t\t\t\t<div class=\"panel panel-info\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t<div class=\"panel-heading\" id=\"detail-panel-source-parent-heading\">IFS overliggende</div>");
            writeLine(writer, "\t\t\t\t\t\t\t\t<div class=\"panel-body\" id=\"detail-panel-ifs-parent\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<form>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t\t<div class=\"form-group\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-source-parent-ebl\">EBL id:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-source-parent-ebl\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-source-parent-description\">Beskrivelse:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-source-parent-description\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t\t</div>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t</form>");
            writeLine(writer, "\t\t\t\t\t\t\t\t</div>");
            writeLine(writer, "\t\t\t\t\t\t\t</div>");

            // N
            writeLine(writer, "\t\t\t</div>");
            writeLine(writer, "\t\t</div>");
            writeLine(writer, "\t</div>");


            writeLine(writer, "\t\t\t\t\t\t</div>");
            writeLine(writer, "\t\t\t\t\t</div>");
            writeLine(writer, "\t\t\t\t\t<div class=\"panel panel-warning\">");
            writeLine(writer, "\t\t\t\t\t\t<div class=\"panel-heading\">Komplettering</div>");
            writeLine(writer, "\t\t\t\t\t\t<div class=\"panel-body\" id=\"detail-panel-interpretation\">");
            writeLine(writer, "\t\t\t\t\t\t\t<form>");
            writeLine(writer, "\t\t\t\t\t\t\t\t<div class=\"form-group\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-complement-type\">Type:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-complement-type\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-complement-typeSequence\">Type sekvens:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-complement-typeSequence\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t</div>");
            writeLine(writer, "\t\t\t\t\t\t\t</form>");
            writeLine(writer, "\t\t\t\t\t\t</div>");
            writeLine(writer, "\t\t\t\t\t</div>");
            writeLine(writer, "\t\t\t\t\t<div class=\"panel panel-success\">");
            writeLine(writer, "\t\t\t\t\t\t<div class=\"panel-heading\">Dynamics</div>");
            writeLine(writer, "\t\t\t\t\t\t<div class=\"panel-body\" id=\"detail-panel-dyn\">");
            writeLine(writer, "\t\t\t\t\t\t\t<form>");
            writeLine(writer, "\t\t\t\t\t\t\t\t<div class=\"form-group\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-target-elementType\">Element type:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-target-elementType\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-target-dynamicsid\">Dynamics ID id:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-target-dynamicsid\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-target-description\">Beskrivelse:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-target-description\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-target-locationType\">Lokasjonstype:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-target-locationType\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-target-assetType\">Objekttype:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-target-assetType\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-target-ebl\">EBL id:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-target-ebl\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<label for=\"detail-panel-target-rds\">RDS id:</label>");
            writeLine(writer, "\t\t\t\t\t\t\t\t\t<input type=\"text\" class=\"form-control\" readonly=\"readonly\" id=\"detail-panel-target-rds\">");
            writeLine(writer, "\t\t\t\t\t\t\t\t</div>");
            writeLine(writer, "\t\t\t\t\t\t\t</form>");
            writeLine(writer, "\t\t\t\t\t\t</div>");
            writeLine(writer, "\t\t\t\t\t</div>");
            writeLine(writer, "\t\t\t\t</div>");
            writeLine(writer, "\t\t\t</div>");
            writeLine(writer, "\t\t</div>");
            writeLine(writer, "\t</div>");
            writeLine(writer, "    <script src=\"https://code.jquery.com/jquery-2.1.1.min.js\" type=\"text/javascript\"></script>");
            writeLine(writer, "    <script src=\"https://cdnjs.cloudflare.com/ajax/libs/bootstrap-treeview/1.2.0/bootstrap-treeview.min.js\" type=\"text/javascript\"></script>");
            writeLine(writer, "    <script type=\"text/javascript\">");
        }
        private void writeBottom(TextWriter writer)
        {
            writeLine(writer, "    </script>");
            writeLine(writer, "</body>");
            writeLine(writer, "</html>");
        }
        private void writeTree(TextWriter writer, IEnumerable<T.IAssetObject> topLevelAssets)
        {
            writeLine(writer, "    $(document).ready(function(){");
            writeLine(writer, "        var treeData = [");

            foreach (T.IAssetObject assetObject in topLevelAssets)
            {
                writeTreeNode(writer, assetObject, 3);
            }

            writeLine(writer, "        ];");
            writeLine(writer, "        $('#myTree').treeview({");
            writeLine(writer, "            data: treeData,");
            writeLine(writer, "            onNodeSelected: function(event, node) {");

            writeLine(writer, "                $('#detail-panel-source-heading').html(node.source.sourceType);");
            writeLine(writer, "                $('#detail-panel-source-ebl').val(node.source.id);");
            writeLine(writer, "                $('#detail-panel-source-description').val(node.source.description);");
            writeLine(writer, "                $('#detail-panel-source-type').val(node.source.type);");

            writeLine(writer, "                $('#detail-panel-source-parent-heading').html(node.source.parent.sourceType + ' overliggende');");
            writeLine(writer, "                $('#detail-panel-source-parent-ebl').val(node.source.parent.id);");
            writeLine(writer, "                $('#detail-panel-source-parent-description').val(node.source.parent.description);");
            writeLine(writer, "                $('#detail-panel-complement-type').val(node.source.complement.type);");
            writeLine(writer, "                $('#detail-panel-complement-typeSequence').val(node.source.complement.typeSequence);");

            
            writeLine(writer, "                $('#detail-panel-target-elementType').val(node.target.elementType);");
            writeLine(writer, "                $('#detail-panel-target-dynamicsid').val(node.target.id);");
            writeLine(writer, "                $('#detail-panel-target-description').val(node.target.description);");
            writeLine(writer, "                $('#detail-panel-target-locationType').val(node.target.locationType);");
            writeLine(writer, "                $('#detail-panel-target-assetType').val(node.target.assetType);");
            writeLine(writer, "                $('#detail-panel-target-ebl').val(node.target.oldId);");
            writeLine(writer, "                $('#detail-panel-target-rds').val(node.target.rdsId);");
            writeLine(writer, "            },");
            writeLine(writer, "            onNodeUnselected: function (event, node) {");
            writeLine(writer, "            }");
            writeLine(writer, "        });");
            writeLine(writer, "    });");
        }
        private void writeTreeNode(TextWriter writer, T.IAssetObject assetObject, int level)
        {
            writeIndent(writer, level); writeLine(writer, "{");
            writeIndent(writer, level + 1); writeLine(writer, $"text: \" {prepareHtmlText($"{assetObject.Id} - {assetObject.Description}")} \",");

            writeIndent(writer, level + 1); writeLine(writer, "source: {");
            writeIndent(writer, level + 2); writeLine(writer, $"id: \"{assetObject.SourceAsset.Id}\",");
            writeIndent(writer, level + 2); writeLine(writer, $"description: \"{prepareHtmlText(assetObject.SourceAsset.Description)}\",");
            writeIndent(writer, level + 2); writeLine(writer, $"type: \"{getType(assetObject.SourceAsset)}\",");
            writeIndent(writer, level + 2); writeLine(writer, $"sourceType: \"{assetObject.SourceAsset.SourceType}\",");

            writeIndent(writer, level + 2); writeLine(writer, "parent: {");
            writeIndent(writer, level + 3); writeLine(writer, $"id: \"{(assetObject.SourceAsset.Parent != null ? assetObject.SourceAsset.Parent.Id : string.Empty)}\",");
            writeIndent(writer, level + 3); writeLine(writer, $"description: \"{(assetObject.SourceAsset.Parent != null ? prepareHtmlText(assetObject.SourceAsset.Parent.Description) : string.Empty)}\",");
            writeIndent(writer, level + 3); writeLine(writer, $"sourceType: \"{(assetObject.SourceAsset.Parent != null ? assetObject.SourceAsset.Parent.SourceType : string.Empty)}\",");
            writeIndent(writer, level + 2); writeLine(writer, "},");

            writeIndent(writer, level + 2); writeLine(writer, "complement: {");
            writeIndent(writer, level + 3); writeLine(writer, $"type: \"{prepareHtmlText(((S.IComplementAssetObject)assetObject.SourceAsset.Complement).Type.ToString())}\",");
            writeIndent(writer, level + 3); writeLine(writer, $"typeSequence: \"{prepareHtmlText(((S.IComplementAssetObject)assetObject.SourceAsset.Complement).TypeSequenceFormatted)}\",");
            writeIndent(writer, level + 2); writeLine(writer, "},");

            writeIndent(writer, level + 1); writeLine(writer, "},");


            writeIndent(writer, level + 1); writeLine(writer, "target: {");

            writeIndent(writer, level + 2); writeLine(writer, $"elementType: \"{assetObject.ElementType}\",");
            writeIndent(writer, level + 2); writeLine(writer, $"id: \"{assetObject.Id}\",");
            writeIndent(writer, level + 2); writeLine(writer, $"description: \"{prepareHtmlText(assetObject.Description)}\",");
            writeIndent(writer, level + 2); writeLine(writer, $"locationType: \"{assetObject.LocationType}\",");
            writeIndent(writer, level + 2); writeLine(writer, $"assetType: \"{assetObject.AssetType}\",");
            writeIndent(writer, level + 2); writeLine(writer, $"oldId: \"{assetObject.SourceAsset.Id}\",");
            writeIndent(writer, level + 2); writeLine(writer, $"rdsId: \"{assetObject.RDSId}\",");
            writeIndent(writer, level + 1); writeLine(writer, "},");

            if (assetObject.Children.Count() > 0)
            {
                writeIndent(writer, level + 1); writeLine(writer, "nodes: [");
                foreach (T.IAssetObject child in assetObject.Children)
                {
                    writeTreeNode(writer, child, level + 2);
                }
                writeIndent(writer, level + 1); writeLine(writer, "]");
            }
            writeIndent(writer, level); writeLine(writer, "},");
        }

        private void writeIndent(TextWriter writer, int level)
        {
            for (int i = 0; i < level; i++)
            {
                writer.Write("\t");
            }
        }

        private void writeLine(TextWriter writer, string line) 
        {
            writer.WriteLine(line);
        }

        private string prepareHtmlText(string input)
        {
            string result = input;

            result = result.Replace("\"", "&quot;");

            return result;
        }
        private string getType(IAssetObject assetObject)
        {
            string result = string.Empty;

            if (assetObject is AssetStructureObject)
            {
                result = ((AssetStructureObject)assetObject).Type.ToString();
            }

            return result;
        }
    }
}
