namespace Pages

open Feliz
open Types
open Fable.Core

open type prop
open type Html
open Browser.Types

module ProfilePage =
    let linhaTabela item = 
        tr [ th [ text item.Id ]
             td [ text item.Nome ]
             td [ text item.Qtd ] ]

    let tabelaRecursos itens  = 
        table [ 
            className "table"
            children [ 
                thead [
                    tr [ th [ text "Id" ]
                         th [ text "Nome" ]
                         th [ abbr [ 
                                title "Quantidade" 
                                text "Qtd" ] ] ] 
                ]
                tbody (Seq.map linhaTabela itens) ]]

    let renderRecursos (username: string) itens = 
        let games = if itens |> Seq.isEmpty then 0 else 2
        let types = itens |> Seq.length
        let total = itens |> Seq.sumBy (fun x -> x.Qtd)
        div [ 
          className "columns"
          children 
            [ div [ 
                className "container profile"
                children
                    [ div [ 
                        className "section profile-heading"
                        children
                          [ div [ 
                                className "columns is-mobile is-multiline"
                                children
                                  [ div [ 
                                        className "column is-2"
                                        children    
                                          [ span [ 
                                            className "header-icon user-profile-image"
                                            children [ img [ alt ""
                                                             src $"https://ui-avatars.com/api/?name={username}" ] ]] ]]
                                    div [ 
                                        className "column is-4-tablet is-10-mobile name"
                                        children [ p [ span [ className "title is-bold"
                                                              children [ text username ] ]]]]
                                    div [ 
                                        className "column is-2-tablet is-4-mobile has-text-centered"
                                        children
                                          [ p [ className "stat-val"
                                                children [text games] ]
                                            p [ className "stat-key"
                                                children [text "Games"] ] ]]
                                    div [ 
                                        className "column is-2-tablet is-4-mobile has-text-centered"
                                        children
                                          [ p [ className "stat-val"
                                                children [text types] ]
                                            p [ className "stat-key"
                                                children [text "Recursos"] ] ]]
                                    div [ 
                                        className "column is-2-tablet is-4-mobile has-text-centered" 
                                        children
                                          [ p [ className "stat-val"
                                                children [text total] ]
                                            p [ className "stat-key"
                                                children [text "Total"] ]]]]]
                            hr []

                            tabelaRecursos itens ]]]]]]

    let loadItens username setItens = 
        promise {
            let! recursos = Api.obterInvetario username 
            setItens recursos
            return ()
        }

    [<ReactComponent>]
    let profile username = 
        let itens,setItens = React.useState(Seq.empty)       

        React.useEffectOnce(fun _ ->  loadItens username setItens |> ignore)
        React.fragment [ renderRecursos username itens ]
