namespace Aisd_1.Tokens;

public class TokenTypeOperatorMultiply : TokenTypeOperator
{
    public override string StringRepresentation => "*";

    public override OperatorKind OperatorKind => OperatorKind.Binary;
}