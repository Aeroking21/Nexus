import { TextArea } from '@microsoft/fast-foundation';
/**
 * Text area appearances
 * @public
 */
export declare type TextAreaAppearance = 'filled' | 'outline';
/**
 * The Fluent Text Area Custom Element. Implements {@link @microsoft/fast-foundation#TextArea},
 * {@link @microsoft/fast-foundation#TextAreaTemplate}
 *
 *
 * @public
 * @remarks
 * HTML Element: \<fluent-text-area\>
 *
 * {@link https://developer.mozilla.org/en-US/docs/Web/API/ShadowRoot/delegatesFocus | delegatesFocus}
 */
export declare class FluentTextArea extends TextArea {
    /**
     * The appearance of the element.
     *
     * @public
     * @remarks
     * HTML Attribute: appearance
     */
    appearance: TextAreaAppearance;
    /**
     * @internal
     */
    appearanceChanged(oldValue: TextAreaAppearance, newValue: TextAreaAppearance): void;
    /**
     * @internal
     */
    connectedCallback(): void;
}
/**
 * Styles for TextArea
 * @public
 */
export declare const TextAreaStyles: import("@microsoft/fast-element").ElementStyles;
