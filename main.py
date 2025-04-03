from functions import encrypt, decrypt
import sys


def main(args: list[str]):
    sys.stdout.reconfigure(encoding='utf-8')

    if len(args) < 5:
        print(
            "Error: Se esperaban 4 argumentos (func: bool, text: string, key: string, encode: bool)",
            file=sys.stderr
        )
        exit(1)
    func: bool = args[1].lower() == 'true'
    text: str = args[2]
    key: str = args[3]
    encode: bool = args[4].lower() == 'true'

    try:
        if func:
            result = encrypt(text, key, encode)
        else:
            result = decrypt(text, key, encode)

        print(result)
    except Exception as e:
        print(f"Error en la ejecución: {e}", file=sys.stderr)
        exit(1)


if __name__ == "__main__":
    main(sys.argv)
