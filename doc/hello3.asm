
.MODEL TINY
.CODE
ORG 100h

START:
    MOV  DX, OFFSET msg
    MOV  AH, 09h
    INT  21h

    MOV  AX, 4C00h
    INT  21h

msg DB 'Hello world', 13, 10, '$'

END START

