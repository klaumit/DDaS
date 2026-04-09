
static void print(const char* msg)
{
    __asm
    {
        mov dx, msg
        mov ah, 09h
        int 21h
    }
}

int main()
{
    print("Hello world\n$");
    return 0;
}

