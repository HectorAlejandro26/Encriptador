from matrix import Matrix
from hashlib import md5, sha256
from constants import VALID_CHARS, UNKOWN_INT, UNKOWN_CHAR


def key_2_matrix(s: str, md5_flag: bool = True) -> Matrix:
    hashcode = (
        md5(s.encode()) if md5_flag else sha256(s.encode())
    ).hexdigest()

    size = 4 if md5_flag else 8
    step = 2 if md5_flag else 1

    # Le da el formato de:
    # 4x4 <= md5
    # 8x8 <= sha256
    key_m_as_list = [
        [int(hashcode[(i * size + j) * step:(i * size + j) * step + step], 16)
         for j in range(size)]
        for i in range(size)
    ]

    return Matrix(key_m_as_list)


def encrypt(text: str, key: str, md5_flag: bool) -> str:
    def char_2_int(c):
        return VALID_CHARS.index(c) if c in VALID_CHARS else UNKOWN_INT

    # Convierte el texto a una lista de números
    numeric_data = [char_2_int(c) for c in text]
    n_cols = 4 if md5_flag else 8  # Si usa md5 4x4, si usa sha256 8x8
    num_rows = (len(numeric_data) + n_cols - 1) // n_cols
    padded_data = numeric_data + \
        [char_2_int('\0')] * (num_rows * n_cols - len(numeric_data))

    # Convierte el texto a una matriz numerica
    matrix_as_list = [
        padded_data[i * n_cols: (i + 1) * n_cols] for i in range(num_rows)]
    text_m = Matrix(matrix_as_list)

    # Se asegura de que la clave tenga el mismo tamaño que la matriz de texto
    key_m = key_2_matrix(key, md5_flag)
    encrypted_m = text_m * key_m

    encrypted_text = "".join(map(chr, encrypted_m.flat))

    return encrypted_text.strip('\0')


def decrypt(text: str, key: str, md5_flag: bool) -> str:
    encrypted = [ord(c) for c in text]
    n_cols = 4 if md5_flag else 8
    num_rows = len(encrypted) // n_cols
    encrypted_m = Matrix(
        [
            encrypted[i * n_cols: (i + 1) * n_cols]
            for i in range(num_rows)
        ]
    )

    key_m_inv = key_2_matrix(key, md5_flag).inv
    if key_m_inv is None:
        raise ValueError(
            "La clave no es válida para descifrar (no invertible)."
        )

    decrypted_m = encrypted_m * key_m_inv

    decrypted_text = "".join(
        map(
            lambda n:
            VALID_CHARS[n % len(VALID_CHARS)] if n >= 0 else UNKOWN_CHAR,
            decrypted_m.flat
        )
    )

    return decrypted_text.strip('\0')
