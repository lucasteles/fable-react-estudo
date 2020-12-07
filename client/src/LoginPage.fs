namespace Pages
open Feliz
open Feliz.Bulma
open System
open type prop
open Browser.Types

module LoginPage =
    let usernameInput (username: string, setUser: string -> unit) = 
        Html.div [
            className "field"
            children [
                Bulma.label [ text "Username" ]
                Bulma.control.div [
                    Bulma.control.hasIconsLeft
                    children [
                        Bulma.input.text [ 
                            placeholder "mariajoao90"
                            required true
                            value username 
                            onChange setUser]
                        Bulma.icon [
                            Bulma.icon.isLeft
                            Bulma.icon.isSmall
                            children [ Html.i [ classes ["fa"; "fa-user"] ] ]
                        ]]]]]

    let passwordInput   = 
        Html.div [
            className "field"
            children [
                Bulma.label [ text "Senha" ]
                Bulma.control.div [
                    Bulma.control.hasIconsLeft
                    children [
                        Bulma.input.password [ placeholder "*******"; required true]
                        Bulma.icon [
                            Bulma.icon.isLeft
                            Bulma.icon.isSmall
                            children [ Html.i [ classes ["fa"; "fa-lock"] ] ]
                        ]]]]]

    let form login =   
        let (user, _) as userState = React.useState("")
        let (error, setError)  = React.useState(false)

        let validate (user: string) = 
            if String.IsNullOrWhiteSpace(user) 
            then setError(true)
            else setError(false)
                 login user

        Html.form [
            className "box"

            children [
                Bulma.title.h2 [
                    Bulma.title.is2
                    color.hasTextDark
                    text "PUJ Login"]

                usernameInput userState
                passwordInput 

                if (error) then 
                    Bulma.notification [
                        Bulma.color.isDanger
                        text "Username invalido" ]

                Bulma.button.button [
                    color.isSuccess
                    text "Login"
                    onClick (fun (e: MouseEvent) -> 
                                    e.preventDefault()
                                    validate user)
                ]
            ]
        ]
