namespace Aisd_1.Tokens;

public class TokenTypeOperatorMinus : TokenTypeOperator
{
    public override string StringRepresentation => "-";
    
    public override OperatorKind OperatorKind => OperatorKind.Binary | OperatorKind.Unary;
}