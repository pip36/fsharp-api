namespace Services

module DictionaryService =
    open Domain.Words.Game
    let private words = [| "pears"; "plums"; "peach" |]

    let getWord () =
        async {
            let rnd = System.Random()
            return Answer(words.[rnd.Next(words.Length)])
        }
