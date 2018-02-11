
module AirportParser

open ParserUtils

open System.Xml

let code = "code";
let name = "name"

type Airport = {
    code: string;
    name: string;
}

let mapToAirport (node: XmlNode) : Airport=  
    { 
        name = node |> getAttributes|> getAttributeValue name
        code = node |> getAttributes|> getAttributeValue code
    }
    
let parseAirports (xml : string) = 
    let doc = xml |> loadDocument
    doc.DocumentElement.ChildNodes
    |> Seq.cast<XmlNode>
    |> Seq.map mapToAirport

