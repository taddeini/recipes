import Head from "next/head";
import Link from "next/link";
import styles from "../styles/Layout.module.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHome } from "@fortawesome/free-solid-svg-icons";

export default function Layout({ children }) {
  return (
    <>
      <Head>
        <title>Recipes</title>
        <meta charSet="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <meta name="description" content="Favorite recipes that load fast" />
        <meta
          httpEquiv="Content-Security-Policy"
          content="default-src 'self' *.aet-software.com; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval'"
        />
      </Head>
      <header className={styles.header}>
        <Link href="/">
          <a>
            <span>
              <FontAwesomeIcon icon={faHome} />
            </span>
            &nbsp;Home
          </a>
        </Link>
      </header>
      <main>{children}</main>
    </>
  );
}
