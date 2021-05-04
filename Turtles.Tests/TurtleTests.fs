module Tests

open Xunit

type Tests(output:ITestOutputHelper) =

    [<Fact>]
    let add_5_to_3_should_be_8() =
        let result = Calculator.add 5 3
        write result
        Assert.Equal(8, result)   