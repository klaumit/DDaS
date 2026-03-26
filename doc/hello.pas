
program hello;
uses
  Crt;
var
  i: Integer;
begin
  Writeln;
  TextColor(Yellow);
  Writeln('Starting here...');
  TextColor(LightGray);
  Writeln;
  for i := 1 to 15 do begin
    TextAttr := i;
    WriteLn('Hello World!');
  end;
  TextAttr := $07;
  WriteLn('Done.');
end.


