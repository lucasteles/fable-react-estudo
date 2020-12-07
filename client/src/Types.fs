namespace Types

type Username = string
type Pages = 
    | Login
    | Profile of Username


type Item = {
    Id: int
    Nome: string
    Qtd: int
}
