from matrix import Matrix
from hashlib import md5, sha256

_CHARS = "\0\n\rABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ÁÉÍÓÚáéíóú@.,?-_/\\ "
_UNKOWN_CHAR = '⍰'
_UNKOWN_INT = -1


def _hash_to_matrix(s: str, md5_flag: bool) -> list[list[int]]:
    """
    Genera una matriz numérica a partir del hash de la cadena de entrada.
    """
    hashcode = md5(s.encode()).hexdigest(
    ) if md5_flag else sha256(s.encode()).hexdigest()
    size = 4 if md5_flag else 8
    step = 2 if md5_flag else 1

    return [
        [int(hashcode[(i * size + j) * step:(i * size + j) * step + step], 16)
         for j in range(size)]
        for i in range(size)
    ]


def _char_to_int(c: str) -> int:
    """Convierte un carácter a su índice en _CHARS, o retorna _UNKOWN_INT si no está presente."""
    return _CHARS.index(c) if c in _CHARS else _UNKOWN_INT


def _int_to_char(n: int) -> str:
    """Convierte un número en un carácter basado en _CHARS, manejando valores desconocidos."""
    return _CHARS[n % len(_CHARS)] if n >= 0 else _UNKOWN_CHAR


def num_2_str(n: int) -> str:
    return _int_to_char(n)


def str_2_num(s: str) -> int:
    if len(s) != 1:
        raise ValueError("La cadena debe contener exactamente un carácter.")
    return _char_to_int(s)


def key_2_matrix(s: str, md5_flag: bool = True) -> Matrix:
    return Matrix(_hash_to_matrix(s, md5_flag))


def text_2_matrix(data: str, md5_flag: bool = True, fill_value: str = '\0') -> Matrix:
    numeric_data = [str_2_num(c) for c in data]
    n_cols = 4 if md5_flag else 8
    num_rows = (len(numeric_data) + n_cols - 1) // n_cols
    padded_data = numeric_data + \
        [str_2_num(fill_value)] * (num_rows * n_cols - len(numeric_data))

    return Matrix([padded_data[i * n_cols: (i + 1) * n_cols] for i in range(num_rows)])


def encrypt(text: str, key: str, md5_flag: bool) -> str:
    return ''.join(chr(c) for c in (text_2_matrix(text, md5_flag, '\0') * key_2_matrix(key, md5_flag)).flat)


def decrypt(text: str, key: str, md5_flag: bool) -> str:
    encrypted = [ord(c) for c in text]
    n_cols = 4 if md5_flag else 8
    num_rows = len(encrypted) // n_cols
    encrypted_m = Matrix([encrypted[i * n_cols: (i + 1) * n_cols]
                         for i in range(num_rows)])

    key_m_inv = key_2_matrix(key, md5_flag).inv
    if key_m_inv is None:
        raise ValueError(
            "La clave no es válida para descifrar (no invertible).")

    return ''.join(num_2_str(c) for c in (encrypted_m * key_m_inv).flat).strip('\0')
