from functions import *
MD5_FLAG = True
"""
``True``: Usa MD5 para la clave (matriz 4x4), numeros entre [0x0 - 0xFF]\n
``False``: Usa SHA-256 para la clave (matriz 8x8), numeros enttre [0x0 - 0xF]
"""


def main():
    text = "Texto a encriptar."
    key = "hola mundo"

    text_m = text_2_matrix(text, MD5_FLAG)
    key_m = key_2_matrix(key, MD5_FLAG)

    encrpited_m = text_m * key_m
    encrpited = ''.join([chr(c) for c in encrpited_m.flat])

    unencripted_m = encrpited_m * key_m.inv
    unencripted = ''.join([num_2_str(c)
                          for c in unencripted_m.flat]).strip('\0')

    print("Texto original: ", text)  # ? Texto a encriptar.
    print("Texto encriptado: ", encrpited)  # ? ䷦ⶏㇳ⻴鈭屁䄹匫傊㦺⸌㒰䙳㾌㖔ㄖ䀤咔┈⨞
    print("Texto desencriptado: ", unencripted)  # ? Texto a encriptar.
    print("Son iguales?", text == unencripted)  # * True


if __name__ == "__main__":
    main()
