module Api
open Types
open Fable.Core

let [<Literal>] url = "https://thunderfarm.azurewebsites.net/api/inventario/"
let [<Literal>] JSON_URL = url + "lucasteles"
type private Invetario = Fable.JsonProvider.Generator<JSON_URL>

let obterInvetario username = 
    async {
        let! (_, res) = Fable.SimpleHttp.Http.get $"{url}/{username}"
        let items = Invetario.ParseArray res
        return items |> Seq.map (fun x -> { Id = int x.recurso.id
                                            Nome = x.recurso.nome 
                                            Qtd = int x.quantidade })
    } 
    |> Async.StartAsPromise
    


