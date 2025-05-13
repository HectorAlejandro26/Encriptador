from functions import encrypt, decrypt
from constants import VALID_CHARS
import sys


def main(args: list[str]):
    sys.stdout.reconfigure(encoding='utf-8')
    # 60% caracteres válidos para considerar cifrado
    MIN_ENCRYPT_RATIO = 0.60
    # 70% caracteres inválidos (30% válidos) para considerar descifraf
    MIN_DECRYPT_RATIO = 0.30

    if len(args) < 4:
        print(
            "Error: Se esperaban 3 argumentos (text: string, key: string, hashType: string)",
            file=sys.stderr
        )
        sys.exit(-1)
    text: str = args[1]
    key: str = args[2]
    if args[3].lower() not in ["sha256", "md5"]:
        print("Error: hashType debe ser sha256 o md5", file=sys.stderr)
        sys.exit(-1)
    hash_type: bool = args[3].lower() == "md5"

    try:
        valid_count = 0
        for c in text:
            if c in VALID_CHARS:
                valid_count += 1

        valid_ratio = valid_count / len(text) if len(text) > 0 else 0
        if valid_ratio < MIN_DECRYPT_RATIO:
            result = decrypt(text, key, hash_type)
            exit_code = 1

        elif valid_ratio > MIN_ENCRYPT_RATIO:
            result = encrypt(text, key, hash_type)
            exit_code = 0

        else:
            raise ValueError(
                "El texto es ambiguo, no se sabe s cifrar o desifrar\n"
                "Intente usar caracteres mas comunes."
            )
        print(result, end='', file=sys.stdout)
        print(f"in: {len(text)}\tout: {len(result)}", file=sys.stderr)
        sys.exit(exit_code)
    except Exception as e:
        print(f"Error en la ejecución: {e}", file=sys.stderr)
        sys.exit(-1)


if __name__ == "__main__":
    main(sys.argv)
