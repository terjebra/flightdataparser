
module ParserUtils

open System.Xml

let loadDocument xml= 
    let doc = XmlDocument()
    doc.LoadXml(xml)
    doc

let getInnerText (node: XmlNode) =
    node.InnerText

let getAttributes (node: XmlNode) =
    node.Attributes

let getNodeAttributes name (node: XmlNode) =
    let nodes = node.ChildNodes |> Seq.cast<XmlNode> |> Seq.filter (fun x -> x.Name = name)
    match nodes |> Seq.length  with
    | 1 ->
        Some (nodes |> Seq.head |> getAttributes)
    | _ ->
        None

let getNodeText name (node: XmlNode) =
    let nodes = node.ChildNodes |> Seq.cast<XmlNode> |> Seq.filter (fun x -> x.Name = name)
    match nodes |> Seq.length  with
    | 1 -> nodes |> Seq.head |> getInnerText |> Some
    | _ ->  None


let findNode name (node: XmlNode) =
    let nodes = node.ChildNodes |> Seq.cast<XmlNode> |> Seq.filter (fun x -> x.Name = name)
    match nodes |> Seq.length  with
    | 1 -> Some (nodes |> Seq.head)
    | _ -> None

let getAttributeValue name (attributes:XmlAttributeCollection) = 
    match attributes.GetNamedItem(name) with
     | null -> ""
     | attr -> attr.Value

let getText text =
    match text with
    | Some value -> value
    | None _ -> ""

