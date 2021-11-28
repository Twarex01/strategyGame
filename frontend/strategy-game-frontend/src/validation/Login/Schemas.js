import * as yup from 'yup'

const validationSchema = yup.object({
    email: yup
    .string('Adja meg email címét')
    .email('Kérem adjon meg egy valós email címet')
    .required("Szükséges mező"),
    password: yup
      .string('Adja meg jelszavát')
      .min(8, 'A jelszónak legalább 8 karakter hosszúnak kell lennie')
      .required('Szükséges mező'),
  });

  export default validationSchema