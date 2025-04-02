from matrix import Matrix
from hashlib import md5, sha256

_CHARS = "\0\n\rABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ÁÉÍÓÚáéíóú@.,?-_/\\ "
"""
_CHARS: str
    Cadena de caracteres permitidos para la conversión entre caracteres y números.
    Incluye caracteres especiales, letras mayúsculas y minúsculas, números y algunos símbolos.
    Ejemplo: ``"\\0\\n\\rABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ÁÉÍÓÚáéíóú@.,?-_/\\ "``
"""
_UNKOWN_CHAR = '░'
"""
_UNKOWN_CHAR: str
    Carácter utilizado como valor predeterminado cuando un número no puede ser convertido a un carácter válido.
"""
_UNKOWN_INT = -1
"""
_UNKOWN_INT: int
    Valor entero utilizado como predeterminado cuando un carácter no puede ser convertido a un número válido.
    Convierte un número entero en un carácter basado en la tabla de caracteres `_CHARS`.
        `n`: Número entero a convertir. El carácter correspondiente al índice `n` en `_CHARS`. Si `n` es negativo, retorna `_UNKOWN_CHAR`.
        >>> num_2_str(0)
        '\0'
        >>> num_2_str(-1)
        '░'
"""


def num_2_str(n: int) -> str:
    """
    Convierte un carácter en su índice correspondiente en la tabla de caracteres `_CHARS`.
        s: Cadena de texto que debe contener exactamente un carácter.\r
        El índice del carácter en `_CHARS`. Si el carácter no está en `_CHARS`, retorna `_UNKOWN_INT`.
    Raises:\n
        ValueError: Si la cadena no contiene exactamente un carácter.\n
        >>> str_2_num('A')
        3\n
        >>> str_2_num('!')
        -1
    """
    if (n < 0):
        return _UNKOWN_CHAR

    index = n % len(_CHARS)
    return _CHARS[index]


def str_2_num(s: str) -> int:
    """
    Convierte una clave de texto en una matriz numérica basada en su hash MD5 o SHA-256.
        s: Cadena de texto que representa la clave.
        md5_flag: Si es True, utiliza MD5 para generar el hash y crea una matriz 4x4.\r
                  Si es False, utiliza SHA-256 y crea una matriz 8x8.\n
        Una instancia de `Matrix` que contiene los valores numéricos derivados del hash.\n
        >>> key_2_matrix("clave", md5_flag=True)
        # Matrix 4x4 con valores numéricos derivados del hash MD5.\n
        >>> key_2_matrix("clave", md5_flag=False)
        # Matrix 8x8 con valores numéricos derivados del hash SHA-256.\n
    """
    if len(s) != 1:
        raise ValueError("La cadena debe contener exactamente un carácter.")

    return _CHARS.index(s) if s in _CHARS else _UNKOWN_INT


def key_2_matrix(s: str, md5_flag: bool = True):
    """
    Convierte una cadena de texto en una matriz numérica de tamaño Nx4 (MD5) o Nx8 (SHA-256).
        `md5_flag`: Si es True, crea una matriz de 4 columnas (MD5). Si es False, 8 columnas (SHA-256).
        `fill_value`: Carácter utilizado para rellenar si la longitud de `data` no es múltiplo del número de columnas.
        Una instancia de `Matrix` que contiene los valores numéricos de la cadena, con dimensiones Nx4 o Nx8.
        Matrix 2x4 con valores ASCII de 'h', 'e', 'l', 'l', 'o', '\\0', '\\0', '\\0'.
    """
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
