from hashlib import md5, sha256
from matrix import Matrix

_chars = "\0\n\rABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ÁÉÍÓÚáéíóú@.,?-_/\\ "
_UNKOWN_CHAR = '░'
_UNKOWN_INT = -1


def num_2_str(n: int) -> str:
    if (n < 0):
        return _UNKOWN_CHAR

    index = n % len(_chars)
    return _chars[index]


def str_2_num(s: str) -> int:
    if len(s) != 1:
        raise ValueError("La cadena debe contener exactamente un carácter.")

    return _chars.index(s) if s in _chars else _UNKOWN_INT


def key_2_matrix(s: str, md5_flag: bool = True):
    """Convierte una cadena en una matriz 4x4 (MD5) o 8x8 (SHA-256) de valores enteros."""
    hashcode = md5(s.encode()).hexdigest(
    ) if md5_flag else sha256(s.encode()).hexdigest()

    size = 4 if md5_flag else 8  # Dimensión de la matriz
    step = 2 if md5_flag else 1  # Paso en los índices

    m: list[list[int]] = []

    for i in range(size):
        row = []
        for j in range(size):
            idx = (i * size + j) * step
            hex_value = hashcode[idx:idx + step]
            row.append(int(hex_value, 16))
        m.append(row)

    return Matrix(m)


def text_2_matrix(
    data: str,
    md5_flag: bool = True,
    fill_value: str = '\0'
) -> Matrix:
    """
    Convierte una cadena en una matriz numérica de tamaño Nx4 (MD5) o Nx8.

    Args:
        data: Cadena de texto a convertir.
        md5_flag: Si True, crea una matriz de 4 columnas (MD5). Si False, 8 columnas.
        fill_value: Valor para rellenar si la longitud de `data` no es múltiplo de las columnas.
                   Por defecto es el carácter nulo ('\\0').

    Returns:
        Matriz de enteros o fracciones, con dimensiones Nx4 o Nx8.

    Example:
        >>> text_2_matrix("hello", md5_flag=True)
        Matriz 2x4 con valores ASCII de 'h', 'e', 'l', 'l', 'o', '\\0', '\\0', '\\0'.
    """
    # Convertir cada carácter a número (ejemplo: usando ord() o str_2_num)
    numeric_data = [str_2_num(c) for c in data]

    n_cols = 4 if md5_flag else 8  # Número de columnas (no filas)
    num_elements = len(numeric_data)

    # Calcular filas necesarias (redondeo hacia arriba)
    num_rows = (num_elements + n_cols - 1) // n_cols

    # Rellenar con fill_value si es necesario
    padded_data = numeric_data + \
        [str_2_num(fill_value)] * (num_rows * n_cols - num_elements)

    # Crear matriz Nx4 u Nx8
    matrix = [
        padded_data[i * n_cols: (i + 1) * n_cols]
        for i in range(num_rows)
    ]

    return Matrix(matrix)
