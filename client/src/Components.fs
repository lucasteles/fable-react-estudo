module Components

open Feliz
open Feliz.Bulma
open Types
open Pages
open type prop

[<ReactComponent>]
let mainContainer childComponent =
    Bulma.section [
      classes ["hero"; "is-primary"; "is-fullheight"]
      children [
        Html.div [
          classes ["hero"; "is-primary"; "is-fullheight"]
          className "hero-body"
          children [
            Html.div [
              className "hero-body"
              children [
                Html.div [
                  className "container"
                  children [
                    Bulma.columns [
                      columns.isCentered
                      children [
                        Bulma.column [
                            column.is9
                            children [childComponent]]]]]]]]]]]]

[<ReactComponent>]
let router loginFn = 
    function
    | Login -> LoginPage.form loginFn
    | Profile username -> ProfilePage.profile username

[<ReactComponent>]
let main () = 
    let page,setPage = React.useState(Pages.Login)
    let login = Profile >> setPage
    Html.div [ router login page |> mainContainer  ]
