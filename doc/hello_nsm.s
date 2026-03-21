
00000000  BA0C01            mov dx,0x10c
00000003  B409              mov ah,0x9
00000005  CD21              int 0x21
00000007  B8004C            mov ax,0x4c00
0000000A  CD21              int 0x21
0000000C  48                dec ax
0000000D  656C              gs insb
0000000F  6C                insb
00000010  6F                outsw
00000011  20776F            and [bx+0x6f],dh
00000014  726C              jc 0x82
00000016  640D0A24          fs or ax,0x240a
