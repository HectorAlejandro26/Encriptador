import numpy as np
from hashlib import md5, sha256
from string import (
    ascii_letters as asci,
    digits as dig,
    punctuation as pt,
    whitespace as ws,
    printable as prnt
)

VALID_CHARS = "".join(
    sorted(set("\0" + asci + dig + pt + ws + prnt)))

# Rango de suplentes a evitar
SURROGATE_RANGE = range(0xD800, 0xE000)


def key_2_matrix(s: str, md5_flag: bool = True) -> np.ndarray:
    hashcode = (md5(s.encode()) if md5_flag else sha256(
        s.encode())).hexdigest()
    size = 3
    values = [int(hashcode[i:i+2], 16) % len(VALID_CHARS)
              for i in range(0, size * size * 2, 2)]
    return np.array(values, dtype=int).reshape((size, size))


def encrypt(text: str, key: str, md5_flag: bool = True) -> str:
    def letras_nums(cadena: str):
        return np.array([VALID_CHARS.index(c) for c in cadena])

    def int_str(lista: list[int]):
        result = []
        for n in lista:
            while n in SURROGATE_RANGE:
                n = (n + 0x800) % 0x10000  # Saltar el rango suplente
            result.append(n)
        return "".join(chr(n) for n in result)

    key_m = key_2_matrix(key, md5_flag)
    text_nums = letras_nums(text)

    # Acomodar en matriz 3xN
    mod = len(text_nums) % key_m.shape[0]
    if mod != 0:
        text_nums = np.append(
            text_nums, [0] * (key_m.shape[0] - mod))

    text_m = np.reshape(text_nums, (key_m.shape[0], -1))
    encrypted_m = np.dot(key_m, text_m)

    return int_str(encrypted_m.flatten())


def decrypt(text: str, key: str, md5_flag: bool = True) -> str:
    def nums_letras(lista: list[int]):
        return "".join(VALID_CHARS[n % len(VALID_CHARS)] for n in lista)

    def str_int(cadena: str):
        return np.array([ord(c) for c in cadena])

    key_m = key_2_matrix(key, md5_flag)
    text_nums = str_int(text)

    # Acomodar matriz en 3xN
    mod = len(text_nums) % key_m.shape[0]
    if mod != 0:
        text_nums = np.append(text_nums, [0] * (key_m.shape[0] - mod))

    text_m = np.reshape(text_nums, (key_m.shape[0], -1))

    try:
        key_m_inv = np.linalg.inv(key_m)
    except np.linalg.LinAlgError:
        raise ValueError("La clave no es válida (no invertible).")

    decrypted_m = np.rint(np.dot(key_m_inv, text_m)).astype(int)

    return nums_letras(decrypted_m.flatten()).strip("\0")
