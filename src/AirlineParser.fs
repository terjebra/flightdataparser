
module AirlineParser

open ParserUtils

open System.Xml

let code = "code";
let name = "name"

type Airline = {
    code: string;
    name: string;
}

let mapToAirline (node: XmlNode) : Airline=  
    { 
        name = node |> getAttributes|> getAttributeValue name
        code = node |> getAttributes|> getAttributeValue code
    }
    
let parseAirlines (xml : string) = 
    let doc = xml |> loadDocument
    doc.DocumentElement.ChildNodes
    |> Seq.cast<XmlNode>
    |> Seq.map mapToAirline

